//-----------------------------------------------------------------------
// <copyright file="CustomerService.cs" company="SEE Software">
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
    using SEE.Framework.Core.Abstract;
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;
    using Microsoft.Extensions.Logging;
    using SEE.Framework.Core.DTO;
    using System.Linq;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using Microsoft.Extensions.Options;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Extensions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SEE.Framework.Core.Collection;
    using SEE.TinyBusinessMarket.BackEnd.Common.Request;
    using SEE.TinyBusinessMarket.BackEnd.Common.Helpers;
    using SEE.TinyBusinessMarket.BackEnd.Common.Consts;

    public class CustomerService : ServiceBase<CustomerService>, ICustomerService
    {
        #region members & ctor
        private readonly IMailService _mailService;
        public CustomerService(ILog<CustomerService> log
            , IOptions<ApplicationConfiguration> options
            , IQuery query
            , IStore store
            , ITransaction transaction
            , IMailService mailService
            )
            : base(log, options, query, store, transaction)
        {
            _mailService = mailService;
        }
        #endregion

        #region register
        public async Task<CommandResult<CustomerUpdateModel>> ValidateAndCreateAsync(CustomerUpdateModel model)
        {
            try
            {
                if (await Validate(model))
                {
                    log.InfoFormat("Registering new Customer: [{0}]", model.Name.Value);
                    var customerEntity = await CreateAsync(model);
                    DataMapper.MapFrom(customerEntity, model);
                    return new CommandResult<CustomerUpdateModel>(model);
                }
                return new CommandResult<CustomerUpdateModel>(model, Localization.Resource.Validation_Summary_Error);
            }
            catch (Exception e)
            {
                log.Error(nameof(ValidateAndCreateAsync), model, e);
                return new CommandResult<CustomerUpdateModel>(model, e);
            }
        }

        public async Task<CustomerEntity> CreateAsync(CustomerUpdateModel model)
        {
            var customerEntity = DataMapper.MapTo(model, new CustomerEntity(), e =>
            {
                e.Id = Guid.NewGuid();
                e.RegisterDate = DateTime.Now;
            });
            await store.CreateAsync(customerEntity);
            return customerEntity;
        }

        public async Task<bool> Validate(CustomerUpdateModel model)
        {
            DataValidator.Validate(model);
            if (!string.IsNullOrWhiteSpace(model.Email.Value) && await query.Get<CustomerEntity>().AnyAsync(x => x.Id != model.Id.Value && x.Email == model.Email.Value))
            {
                model.Email.AddError(Localization.Resource.Validation_EmailExists);
            }
            return DataValidator.IsValid(model);
        }
        #endregion

        #region details & list
        public async Task<ProfileContract> GetProfileAsync(Guid customerId)
        {
            return new ProfileContract
            {
                Customer = await query.Get<CustomerEntity>().Where(x => x.Id == customerId).SelectCustomerContract().SingleAsync(),
                Invoices = await query.Get<InvoiceEntity>().Where(x => x.Licence.CustomerId == customerId).OrderByDescending(x => x.SellDate).SelectInvoiceContract().ToListAsync(),
                Licences = await query.Get<LicenceEntity>().Where(x => x.CustomerId == customerId).OrderByDescending(x => x.ValidFrom).SelectLicenceContract().ToListAsync(),
            };
        }

        public async Task<DataResponse<CustomerRequest, CustomerContract>> FindBy(CustomerRequest request)
        {
            return await query.Get<CustomerEntity>().FindBy(request).SortBy(request).SelectCustomerContract().ToDataResponseAsync(request);
        }

        public async Task<CustomerContract> GetDetailsAsync(Guid customerId)
        {
            return await query.Get<CustomerEntity>().Where(x => x.Id == customerId).SelectCustomerContract().SingleAsync();
        }

        public async Task<IReadOnlyList<OrderContract>> Orders(Guid customerId)
        {
            return await query.Get<OrderEntity>().Where(x => x.CustomerId == customerId).OrderByDescending(x => x.Start).SelectOrderContract().ToListAsync();
        }

        public async Task<IReadOnlyList<LicenceContract>> Licences(Guid customerId)
        {
            return await query.Get<LicenceEntity>().Where(x => x.CustomerId == customerId).OrderByDescending(x => x.ValidFrom).SelectLicenceContract().ToListAsync();
        }
        #endregion

        public async Task<CommandResult> PasswordAsync(Guid customerId)
        {
            try
            {
                using (var transaction = transactionScope.BeginTransaction())
                {
                    var now = DateTime.Now;
                    var tokenEntity = new TokenEntity
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customerId,
                        Active = true,
                        Created = now,
                        Data = Tokenizer.Token(),
                        Expires = now.AddHours(SettingConsts.TokenPeriod),
                    };
                    await store.CreateAsync(tokenEntity);
                    // Send mail with token link
                    await _mailService.SendTokenAsync(tokenEntity.Id);
                    transaction.Commit();
                    return CommandResult.Ok;
                }
            }
            catch (Exception e)
            {
                log.Error(nameof(PasswordAsync), e);
                return new CommandResult(e);
            }
        }
    }
}
