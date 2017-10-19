//-----------------------------------------------------------------------
// <copyright file="PaymentService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Service
{
    using SEE.TinyBusinessMarket.BackEnd.BLL.Base;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using SEE.Framework.Core.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;
    using SEE.Framework.Core.DTO;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System.Linq;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Extensions;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using SEE.TinyBusinessMarket.BackEnd.Common.Enum;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using Microsoft.Extensions.Options;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Payu;
    using SEE.TinyBusinessMarket.BackEnd.Common.Helpers;
    using System.Runtime.Loader;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class PaymentService : ServiceBase<PaymentService>, IPaymentService
    {
        #region members & ctor
        private readonly ICustomerService _customerService;
        private readonly IPayuManager _payuManager;
        private readonly IMailService _mailService;
        public PaymentService(ILog<PaymentService> log
            , IOptions<ApplicationConfiguration> options
            , IQuery query
            , IStore store
            , ITransaction transaction
            , ICustomerService customerService
            , IPayuManager payuManager
            , IMailService mailService
            )
            : base(log, options, query, store, transaction)
        {
            _customerService = customerService;
            _payuManager = payuManager;
            _mailService = mailService;
        }
        #endregion

        public async Task<PaymentUpdateModel> AddAsync(Guid productId)
        {
            log.InfoFormat("Start with: {0}", productId);
            return new PaymentUpdateModel
            {
                Product = await query.Get<ProductEntity>().Where(x => x.Id == productId).SelectProductContract().SingleAsync(),
                Customer = new CustomerUpdateModel(),
            };
        }

        #region PayU
        public async Task<CommandResult<PaymentUpdateModel>> StartOrderAsync(PaymentUpdateModel model)
        {
            try
            {
                if (Validate(model))
                {
                    using (var transaction = transactionScope.BeginTransaction())
                    {
                        var productContract = query.Get<ProductEntity>().Where(x => x.Id == model.Product.Id).SelectProductContract().Single();
                        var customerEntity = await _customerService.CreateAsync(model.Customer);
                        var orderEntity = new OrderEntity
                        {
                            Id = Guid.NewGuid(),
                            CustomerId = customerEntity.Id,
                            ProductId = model.Product.Id,
                            Start = DateTime.Now,
                            OrderStatusId = (int)OrderStatusEnum.PENDING,
                            Token = Tokenizer.Token(),
                            OrderTypeId = (int)OrderTypeEnum.PAYU,
                            Quantity = 1,
                        };
                        store.Create(orderEntity);
                        // Send request to PayU
                        var payuProduct = new PayuProductContract
                        {
                            Name = productContract.FullName,
                            Quantity = 1,
                            UnitPrice = (int)(productContract.GrossPrice * 100),
                        };
                        var payuBuyer = new PayuBuyerContract
                        {
                            FirstName = customerEntity.FirstName,
                            LastName = customerEntity.LastName,
                            Email = customerEntity.Email,
                            Phone = customerEntity.Phone,
                            Language = PayuConsts.Language,
                        };
                        var payuResult = await _payuManager.CreateOrderAsync(orderEntity.Token, payuProduct, payuBuyer);
                        if (payuResult.Succeeded)
                        {
                            var payuOutput = payuResult.Value;
                            model.RedirectUrl = payuOutput.RedirectUri;
                        }
                        else
                        {
                            orderEntity.OrderStatusId = (int)OrderStatusEnum.PENDING;
                            store.Update(orderEntity);
                            // cancel order
                        }
                        transaction.Commit();
                        return new CommandResult<PaymentUpdateModel>(model);
                    }
                }
                return new CommandResult<PaymentUpdateModel>(model, Localization.Resource.Validation_Summary_Error);
            }
            catch (Exception e)
            {
                log.Error(nameof(StartOrderAsync), model, e);
                return new CommandResult<PaymentUpdateModel>(model, e);
            }
        }

        public async Task<CommandResult> FinishOrderAsync(string token)
        {
            try
            {
                var orderEntity = query.SingleOrDefault<OrderEntity>(x => x.Token == token && x.OrderStatusId == (int)OrderStatusEnum.PENDING);
                if (orderEntity != null)
                {
                    return await FinishOrderAsync(orderEntity, DateTime.Now);
                }
                return new CommandResult(Localization.Resource.Validation_OrderExpired);
            }
            catch (Exception e)
            {
                log.Error(nameof(FinishOrderAsync), e);
                return new CommandResult(e);
            }
        }

        public async Task<CommandResult<FinishOrderUpdateModel>> FinishOrderAsync(FinishOrderUpdateModel model)
        {
            try
            {
                if (DataValidator.Validate(model))
                {
                    var orderEntity = query.SingleOrDefault<OrderEntity>(x => x.Id == model.Id && x.OrderStatusId == (int)OrderStatusEnum.PENDING);
                    if (orderEntity != null)
                    {
                        var result = await FinishOrderAsync(orderEntity, model.SellDate.Value.Value);
                        if (result.Succeeded)
                        {
                            return new CommandResult<FinishOrderUpdateModel>(model);
                        }
                        return new CommandResult<FinishOrderUpdateModel>(model, result.ErrorMessage);
                    }
                }
                return new CommandResult<FinishOrderUpdateModel>(model, Localization.Resource.Validation_Summary_Error);
            }
            catch (Exception e)
            {
                log.Error(nameof(FinishOrderAsync), e);
                return new CommandResult<FinishOrderUpdateModel>(model, e);
            }
        }

        private async Task<CommandResult> FinishOrderAsync(OrderEntity orderEntity, DateTime sellDate)
        {
            using (var transaction = transactionScope.BeginTransaction())
            {
                var settingEntity = query.Single<SettingEntity>(x => x.Id == SettingConsts.Current);
                settingEntity.InvoiceLastNumber++;

                var now = DateTime.Now;
                orderEntity.Finish = now;
                orderEntity.OrderStatusId = (int)OrderStatusEnum.COMPLETED;
                store.Update(orderEntity);
                var licenceEntity = new LicenceEntity
                {
                    Id = Guid.NewGuid(),
                    CustomerId = orderEntity.CustomerId,
                    ProductId = orderEntity.ProductId,
                    ValidFrom = now,
                    ValidTo = now.AddYears(1),
                    Quantity = 1,
                };
                store.Create(licenceEntity);
                var tokenEntity = new TokenEntity
                {
                    Id = Guid.NewGuid(),
                    CustomerId = orderEntity.CustomerId,
                    Active = true,
                    Created = now,
                    Data = Tokenizer.Token(),
                    Expires = now.AddHours(SettingConsts.TokenPeriod),
                };
                store.Create(tokenEntity);
                var invoiceEntity = new InvoiceEntity
                {
                    LicenceId = licenceEntity.Id,
                    ExposureDate = sellDate,
                    SellDate = sellDate,
                    PaymentDays = 0,
                    PaymentDate = sellDate,
                    PaymentTypeId = (int)PaymentTypeEnum.Cash,
                    Number = InvoiceNumberBuilder.Build(settingEntity.InvoiceLastNumber, now),
                };
                store.Create(invoiceEntity);

                // update settings
                store.Update(settingEntity);

                // Send mail with token link
                await _mailService.SendTokenAsync(tokenEntity.Id);
                // Send mail with invoice
                await _mailService.SendInvoiceAsync(invoiceEntity.LicenceId);

                transaction.Commit();
                return CommandResult.Ok;
            }
        }
        #endregion

        #region Classic remitance
        public async Task<CommandResult<PaymentUpdateModel>> RemitanceAsync(PaymentUpdateModel model)
        {
            try
            {
                if (Validate(model))
                {
                    using (var transaction = transactionScope.BeginTransaction())
                    {
                        var now = DateTime.Now;
                        var productContract = await query.Get<ProductEntity>().Where(x => x.Id == model.Product.Id).SelectProductContract().SingleAsync();
                        var customerEntity = await _customerService.CreateAsync(model.Customer);
                        // Create order
                        var orderEntity = new OrderEntity
                        {
                            Id = Guid.NewGuid(),
                            CustomerId = customerEntity.Id,
                            ProductId = model.Product.Id,
                            Start = now,
                            OrderStatusId = (int)OrderStatusEnum.PENDING,
                            Token = Tokenizer.Token(),
                            OrderTypeId = (int)OrderTypeEnum.REMITANCE,
                            Quantity = 1,
                        };
                        await store.CreateAsync(orderEntity);
                        // Create pro forma invoice
                        var lastNumber = query.Get<ProFormaEntity>().Min(x => (int?)x.Number) ?? 0;
                        var proFormaEntity = new ProFormaEntity
                        {
                            OrderId = orderEntity.Id,
                            ExposureDate = now,
                            Number = lastNumber + 1,
                            FullNumber = InvoiceNumberBuilder.Build(lastNumber + 1, now),
                        };
                        await store.CreateAsync(proFormaEntity);
                        // Send mail with pro forma
                        await _mailService.SendProFormaAsync(orderEntity.Id);

                        transaction.Commit();
                        return new CommandResult<PaymentUpdateModel>(model);
                    }

                }
                return new CommandResult<PaymentUpdateModel>(model, Localization.Resource.Validation_Summary_Error);
            }
            catch (Exception e)
            {
                log.Error(nameof(RemitanceAsync), model, e);
                return new CommandResult<PaymentUpdateModel>(model, e);
            }
        }
        #endregion

        public bool Validate(PaymentUpdateModel model)
        {
            DataValidator.Validate(model);
            _customerService.Validate(model.Customer);
            return DataValidator.IsValid(model) && DataValidator.IsValid(model.Customer);
        }

        public async Task<CommandResult> SendProFormaAsync(Guid proFormaId)
        {
            try
            {
                await _mailService.SendProFormaAsync(proFormaId);
                return CommandResult.Ok;
            }
            catch (Exception e)
            {
                log.Error(nameof(SendProFormaAsync), e);
                return new CommandResult(e);
            }
        }
    }


}
