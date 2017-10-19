//-----------------------------------------------------------------------
// <copyright file="MailService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Service
{
    using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Base;
    using System;
    using Microsoft.Extensions.Logging;
    using SEE.Framework.Core.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Template;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Mail;
    using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
    using Microsoft.AspNetCore.Http;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System.Linq;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Extensions;
    using SEE.TinyBusinessMarket.BackEnd.Common.Mail;
    using SEE.TinyBusinessMarket.BackEnd.Common.Pdf;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Pdf;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using Microsoft.EntityFrameworkCore;

    public class MailService : ServiceBase<MailService>, IMailService
    {
        #region members & ctor
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITemplateRepository _templateRepository;
        private readonly IMailSender _mailSender;
        private readonly IInvoiceService _invoiceService;
        public MailService(ILog<MailService> log
            , IOptions<ApplicationConfiguration> options
            , IQuery query
            , IStore store
            , ITransaction transactionScope
            , IHttpContextAccessor httpContextAccessor
            , ITemplateRepository templateRepository
            , IMailSender mailSender
            , IInvoiceService invoiceService
            )
            : base(log, options, query, store, transactionScope)
        {
            _httpContextAccessor = httpContextAccessor;
            _templateRepository = templateRepository;
            _mailSender = mailSender;
            _invoiceService = invoiceService;
        }
        #endregion

        #region Base url
        public string BaseUrl
        {
            get
            {
                var request = _httpContextAccessor.HttpContext.Request;
                string baseUrl = $"{request.Scheme}://{request.Host}";
                if (!string.IsNullOrWhiteSpace(request.PathBase))
                {
                    baseUrl = $"{baseUrl}/{request.PathBase}";
                }
                return baseUrl;
            }
        }
        #endregion

        public async Task SendDownloadTokenAsync(Guid tokenId)
        {
            // Parse template with data
            var tokenContract = query.Get<TokenEntity>().Where(x => x.Active && x.Id == tokenId).SelectTokenContract().Single();
            string downloadMail = await _templateRepository.RenderTemplateAsync<DownloadMailModel>(TemplateConsts.MailDownload, new DownloadMailModel
            {
                BaseUrl = BaseUrl,
                Token = tokenContract,
            });
            await _mailSender.SendAsync(tokenContract.CustomerEmail, Localization.Resource.MailSubject_DownloadToken, downloadMail);
        }

        public async Task SendTokenAsync(Guid tokenId)
        {
            var tokenContract = query.Get<TokenEntity>().Where(x => x.Active && x.Id == tokenId).SelectTokenContract().Single();
            string downloadMail = await _templateRepository.RenderTemplateAsync<DownloadMailModel>(TemplateConsts.MailToken, new DownloadMailModel
            {
                BaseUrl = BaseUrl,
                Token = tokenContract,
            });
            await _mailSender.SendAsync(tokenContract.CustomerEmail, Localization.Resource.MailSubject_GeneratedToken, downloadMail);
        }

        public async Task SendInvoiceAsync(Guid invoiceId)
        {
            string invoiceMail = await _templateRepository.RenderTemplateAsync<InvoiceMailModel>(TemplateConsts.MailInvoice, new InvoiceMailModel
            {

            });

            var invoiceDownloadContract = await _invoiceService.DownloadInvoiceAsync(invoiceId);
            if (invoiceDownloadContract.Succeeded)
            {
                string customerEmail = query.Get<InvoiceEntity>().Where(x => x.LicenceId == invoiceId).Select(x => x.Licence.Customer.Email).Single();
                await _mailSender.SendAsync(customerEmail, Localization.Resource.MailSubject_Invoice, invoiceMail, invoiceDownloadContract.Value.Data, invoiceDownloadContract.Value.FileName);
                return;
            }
            throw new Exception(invoiceDownloadContract.ErrorMessage);
        }

        public async Task SendProFormaAsync(Guid orderId)
        {
            string invoiceMail = await _templateRepository.RenderTemplateAsync<InvoiceMailModel>(TemplateConsts.MailProForma, new InvoiceMailModel
            {

            });

            var proFormaDownloadContract = await _invoiceService.DownloadProFormaAsync(orderId);
            if (proFormaDownloadContract.Succeeded)
            {
                string customerEmail = await query.Get<OrderEntity>().Where(x => x.Id == orderId).Select(x => x.Customer.Email).SingleAsync();
                await _mailSender.SendAsync(customerEmail, Localization.Resource.MailSubject_ProForma, invoiceMail, proFormaDownloadContract.Value.Data, proFormaDownloadContract.Value.FileName);
                return;
            }
            throw new Exception(proFormaDownloadContract.ErrorMessage);
        }

    }
}
