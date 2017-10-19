//-----------------------------------------------------------------------
// <copyright file="IdentityService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Service
{
    using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Base;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using SEE.Framework.Core.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System.Linq;
    using System.Security.Claims;
    using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Extensions;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SEE.TinyBusinessMarket.BackEnd.Common.Helpers;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using Microsoft.AspNetCore.Authentication;

    public class IdentityService : ServiceBase<IdentityService>, IIdentityService
    {
        #region members & ctor
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailService _mailService;
        public IdentityService(ILog<IdentityService> log
            , IOptions<ApplicationConfiguration> options
            , IQuery query
            , IStore store
            , ITransaction transactionScope
            , IHttpContextAccessor httpContextAccessor
            , IMailService mailService
            )
            : base(log, options, query, store, transactionScope)
        {
            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
        }
        #endregion

        public async Task<CommandResult> LoginBySecretAsync(string secret)
        {
            try
            {
                var now = DateTime.Now;
                var tokenEntity = query.Get<TokenEntity>().Where(x => x.Active && x.Data == secret && x.Expires >= now).SingleOrDefault();
                if (tokenEntity != null)
                {
                    var customerContract = query.Get<CustomerEntity>().Where(x => x.Id == tokenEntity.CustomerId).SelectCustomerContract().Single();
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, customerContract.Name),
                        new Claim(ClaimTypes.NameIdentifier, customerContract.Id.ToString()),
                        new Claim(ClaimTypes.Role, IdentityConsts.RoleCustomer),
                        new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", IdentityConsts.IdentityInstanceClientCookieName),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
                    await _httpContextAccessor.HttpContext.SignInAsync(IdentityConsts.IdentityInstanceClientCookieName, new ClaimsPrincipal(claimsIdentity));
                    tokenEntity.Active = false;
                    store.Update(tokenEntity);
                    return CommandResult.Ok;
                }
                return new CommandResult(Localization.Resource.Token_Inactive);
            }
            catch (Exception e)
            {
                log.Error(nameof(LoginBySecretAsync), e);
                return new CommandResult(e);
            }
        }

        public async Task<CommandResult> GenerateSecretAsync(string emailAddress)
        {
            try
            {
                var customerEntity = await query.SingleOrDefaultAsync<CustomerEntity>(x => x.Email == emailAddress);
                if (customerEntity != null)
                {
                    using (var transaction = transactionScope.BeginTransaction())
                    {
                        // deactivate existing tokens
                        var existingTokens = await query.Get<TokenEntity>().Where(x => x.CustomerId == customerEntity.Id && x.Active).ToListAsync();
                        foreach (var tokenEntity in existingTokens)
                        {
                            tokenEntity.Active = false;
                            store.Modify(tokenEntity);
                        }
                        var now = DateTime.Now;
                        var newTokenEntity = new TokenEntity
                        {
                            Id = Guid.NewGuid(),
                            CustomerId = customerEntity.Id,
                            Active = true,
                            Created = now,
                            Expires = now.AddHours(SettingConsts.TokenPeriod),
                            Data = Tokenizer.Token(),
                        };
                        store.Add(newTokenEntity);
                        await store.SaveAsync();
                        await _mailService.SendTokenAsync(newTokenEntity.Id);
                        transaction.Commit();
                        return CommandResult.Ok;
                    }
                }
                return new CommandResult(Localization.Resource.Token_EmailNotExists);
            }
            catch (Exception e)
            {
                log.Error(nameof(GenerateSecretAsync), e);
                return new CommandResult(e);
            }
        }

        public async Task<CommandResult> LoginAsync(LoginContract model)
        {
            try
            {
                var adminEntity = await query.SingleOrDefaultAsync<AdminEntity>(x => x.Login == model.Login);
                if (adminEntity != null)
                {
                    if (PasswordHash.ValidatePassword(model.Password, adminEntity.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, adminEntity.Login),
                            new Claim(ClaimTypes.NameIdentifier, adminEntity.Id.ToString()),
                            new Claim(ClaimTypes.Role, IdentityConsts.RoleAdmin),
                            new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", IdentityConsts.IdentityInstanceClientCookieName),
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
                        await _httpContextAccessor.HttpContext.SignInAsync(IdentityConsts.IdentityInstanceClientCookieName, new ClaimsPrincipal(claimsIdentity));
                        return CommandResult.Ok;
                    }
                }
                return new CommandResult(Localization.Resource.Validation_BadLoginOrPassword);

            }
            catch (Exception e)
            {
                log.Error(nameof(LoginAsync), e);
                return new CommandResult(e);
            }
        }
    }
}
