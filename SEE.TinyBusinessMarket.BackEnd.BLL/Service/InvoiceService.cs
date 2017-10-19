//-----------------------------------------------------------------------
// <copyright file="InvoiceService.cs" company="SEE Software">
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
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using Microsoft.Extensions.Options;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Extensions;
    using SEE.TinyBusinessMarket.BackEnd.Common.Pdf;
    using SEE.Framework.Core.Extensions;
    using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Pdf;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Template;
    using SEE.TinyBusinessMarket.BackEnd.Common.Base;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class InvoiceService : ServiceBase<InvoiceService>, IInvoiceService
    {
        #region members & ctor
        private readonly IPdfCreator _pdfCreator;
        private readonly ITemplateRepository _templateRepository;

        public InvoiceService(ILog<InvoiceService> log
            , IOptions<ApplicationConfiguration> options
            , IQuery query
            , IStore store
            , ITransaction transactionScope
            , IPdfCreator pdfCreator
            , ITemplateRepository templateRepository
            )
            : base(log, options, query, store, transactionScope)
        {
            _pdfCreator = pdfCreator;
            _templateRepository = templateRepository;
        }
        #endregion

        #region invoice pdf model
        private InvoicePdfModel GetInvoicePdfModel(Guid invoiceId)
        {
            var invoiceContract = query.Get<InvoiceEntity>().Where(x => x.LicenceId == invoiceId).SelectInvoiceContract().Single();
            var licenceContract = query.Get<LicenceEntity>().Where(x => x.Id == invoiceId).SelectLicenceContract().Single();
            var customerContract = query.Get<CustomerEntity>().Where(x => x.Id == licenceContract.CustomerId).SelectCustomerContract().Single();
            var invoicePdfModel = new InvoicePdfModel
            {
                Customer = customerContract,
                InvoiceConfig = configuration.InvoiceConfiguration,
                PaymentTypeCaption = invoiceContract.PaymentTypeCaption,
                PaymentTypeCode = invoiceContract.PaymentTypeCode,
                PaymentDays = invoiceContract.PaymentDays,
                ExposureDate = invoiceContract.ExposureDate,
                SellDate = invoiceContract.SellDate,
                Number = invoiceContract.Number,
                GrossAmountStringified = licenceContract.ProductGrossPrice.ToStringify(),
                Products = new List<InvoiceProductPdfModel>
                {
                    new InvoiceProductPdfModel
                    {
                        MeasureUnitCaption = InvoiceConsts.DefaultMeasureUnit,
                        Pkwiu = string.Empty,
                        Name = licenceContract.ProductFullName,
                        Quantity = licenceContract.Quantity,
                        VatRateCaption = licenceContract.ProductVatRateName,
                        UnitPrice = licenceContract.ProductUnitPrice,
                        VatRateValue = licenceContract.ProductVatRateValue,
                    }
                },
                Rates = new List<InvoiceVatRatePdfModel>
                {
                    new InvoiceVatRatePdfModel
                    {
                        UnitPrice = licenceContract.ProductUnitPrice,
                        Quantity = licenceContract.Quantity,
                        VatRateValue = licenceContract.ProductVatRateValue,
                        VatRateCaption = licenceContract.ProductVatRateName,
                    }
                }
            };

            return invoicePdfModel;
        }
        #endregion

        #region pro forma pdf model
        private async Task<InvoicePdfModel> GetProFormaPdfModel(Guid orderId)
        {
            var orderContract = await query.Get<OrderEntity>().Where(x => x.Id == orderId).SelectOrderContract().SingleAsync();
            var customerContract = await query.Get<CustomerEntity>().Where(x => x.Id == orderContract.CustomerId).SelectCustomerContract().SingleAsync();
            var productContract = await query.Get<ProductEntity>().Where(x => x.Id == orderContract.ProductId).SelectProductContract().SingleAsync();
            var proFormaEntity = await query.SingleAsync<ProFormaEntity>(x => x.OrderId == orderId);
            var invoicePdfModel = new InvoicePdfModel
            {
                Customer = customerContract,
                InvoiceConfig = configuration.InvoiceConfiguration,
                ExposureDate = proFormaEntity.ExposureDate,
                Number = proFormaEntity.FullNumber,
                GrossAmountStringified = productContract.GrossPrice.ToStringify(),
                Products = new List<InvoiceProductPdfModel>
                {
                    new InvoiceProductPdfModel
                    {
                        MeasureUnitCaption = InvoiceConsts.DefaultMeasureUnit,
                        Pkwiu = string.Empty,
                        Name = productContract.FullName,
                        Quantity = orderContract.Quantity,
                        VatRateCaption = productContract.VatRateName,
                        UnitPrice = productContract.UnitPrice,
                        VatRateValue = productContract.VatRateValue,
                    }
                },
                Rates = new List<InvoiceVatRatePdfModel>
                {
                    new InvoiceVatRatePdfModel
                    {
                        UnitPrice = productContract.UnitPrice,
                        Quantity = orderContract.Quantity,
                        VatRateValue = productContract.VatRateValue,
                        VatRateCaption = productContract.VatRateName,
                    }
                }
            };

            return invoicePdfModel;
        }
        #endregion


        public async Task<CommandResult<DownloadContract>> DownloadInvoiceAsync(Guid invoiceId)
        {
            var invoiceModel = GetInvoicePdfModel(invoiceId);
            string invoiceView = await _templateRepository.RenderTemplateAsync<InvoicePdfModel>(TemplateConsts.PdfInvoice, invoiceModel);
            var result = new DownloadContract
            {
                FileName = $"Faktura_VAT_{invoiceModel.Number.Replace("/", "_")}.pdf",
                Data = _pdfCreator.Invoice(invoiceModel),
                ContentType = "application/pdf",
            };
            return new CommandResult<DownloadContract>(result);
        }
        public async Task<CommandResult<DownloadContract>> DownloadInvoiceAsync(string token, Guid invoiceId)
        {
            var tokenEntity = query.SingleOrDefault<TokenEntity>(x => x.Data == token && x.Active);
            if (tokenEntity != null && query.Get<InvoiceEntity>().Any(x => x.LicenceId == invoiceId && x.Licence.CustomerId == tokenEntity.CustomerId))
            {
                return await DownloadInvoiceAsync(invoiceId);
            }
            return new CommandResult<DownloadContract>(null, Localization.Resource.Token_Inactive);
        }

        public async Task<CommandResult<DownloadContract>> DownloadProFormaAsync(Guid orderId)
        {
            var proFormaModel = await GetProFormaPdfModel(orderId);
            string proFormaView = await _templateRepository.RenderTemplateAsync<InvoicePdfModel>(TemplateConsts.PdfProForma, proFormaModel);
            var result = new DownloadContract
            {
                FileName = $"Faktura_PROFORMA_{proFormaModel.Number.Replace("/", "_")}.pdf",
                Data = _pdfCreator.ProForma(proFormaModel),
                ContentType = "application/pdf",
            };
            return new CommandResult<DownloadContract>(result);
        }
    }
}
