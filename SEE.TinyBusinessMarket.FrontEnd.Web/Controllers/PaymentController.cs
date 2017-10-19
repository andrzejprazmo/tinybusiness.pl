using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;
using SEE.Framework.Core.Common;
using SEE.TinyBusinessMarket.BackEnd.Common.Contract;

namespace SEE.TinyBusinessMarket.FrontEnd.Web.Controllers
{
    public class PaymentController : Controller
    {
        #region members & ctor
        private readonly IPaymentService _paymentService;
        private readonly IMailService _mailService;
        private readonly IInvoiceService _invoiceService;

        public PaymentController(IPaymentService paymentService, IMailService mailService, IInvoiceService invoiceService)
        {
            _paymentService = paymentService;
            _mailService = mailService;
            _invoiceService = invoiceService;
        }
        #endregion

        [HttpGet]
        public async Task<PaymentUpdateModel> Add(Guid productId) => await _paymentService.AddAsync(productId);

        [HttpPost]
        public async Task<CommandResult<PaymentUpdateModel>> Start([FromBody]PaymentUpdateModel model) => await _paymentService.StartOrderAsync(model);

        [HttpGet]
        public async Task<CommandResult> Finish([FromQuery]string token) => await _paymentService.FinishOrderAsync(token);

        [HttpPost]
        public async Task<CommandResult<PaymentUpdateModel>> Remitance([FromBody]PaymentUpdateModel model) => await _paymentService.RemitanceAsync(model);

    }
}