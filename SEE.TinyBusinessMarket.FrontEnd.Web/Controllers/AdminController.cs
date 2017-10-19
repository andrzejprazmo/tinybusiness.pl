using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
using SEE.TinyBusinessMarket.BackEnd.Common.Helpers;
using System.IO;
using SEE.Framework.Core.Collection;
using SEE.TinyBusinessMarket.BackEnd.Common.Request;
using SEE.Framework.Core.Common;
using SEE.TinyBusinessMarket.BackEnd.Common.Base;
using Microsoft.AspNetCore.Authentication;
using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;

namespace SEE.TinyBusinessMarket.FrontEnd.Web.Controllers
{
    [Authorize(Roles = IdentityConsts.RoleAdmin)]
    public class AdminController : Controller
    {
        #region members & ctor
        private readonly IIdentityService _identityService;
        private readonly ICustomerService _customerService;
        private readonly IPaymentService _paymentService;
        private readonly IDownloadService _downloadService;
        private readonly IInvoiceService _invoiceService;
        public AdminController(IIdentityService identityService, ICustomerService customerService, IPaymentService paymentService, IDownloadService downloadService, IInvoiceService invoiceService)
        {
            _identityService = identityService;
            _customerService = customerService;
            _paymentService = paymentService;
            _downloadService = downloadService;
            _invoiceService = invoiceService;
        }
        #endregion

        #region authorization & main page
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginContract model)
        {
            var result = await _identityService.LoginAsync(model);
            if (result.Succeeded)
            {
                // redirect
                string redirect = Path.Combine(Request.PathBase + "/#/admin");
                return Redirect(redirect);
            }
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConsts.IdentityInstanceClientCookieName);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region customer list & details
        [HttpPost]
        public async Task<DataResponse<CustomerRequest, CustomerContract>> Customers([FromBody]CustomerRequest request) => await _customerService.FindBy(request);

        [HttpGet]
        public async Task<CustomerContract> CustomerDetails([FromQuery]Guid customerId) => await _customerService.GetDetailsAsync(customerId);

        [HttpGet]
        public async Task<IReadOnlyList<OrderContract>> CustomerOrders([FromQuery]Guid customerId) => await _customerService.Orders(customerId);

        [HttpGet]
        public async Task<IReadOnlyList<LicenceContract>> CustomerLicences(Guid customerId) => await _customerService.Licences(customerId);
        #endregion

        #region commands
        [HttpPost]
        public async Task<CommandResult> SendProForma([FromBody]GuidContract proFormaId) => await _paymentService.SendProFormaAsync(proFormaId.Id);

        [HttpPost]
        public async Task<CommandResult<FinishOrderUpdateModel>> FinishOrder([FromBody]FinishOrderUpdateModel model) => await _paymentService.FinishOrderAsync(model);

        [HttpGet]
        public async Task<IActionResult> DownloadInvoice(Guid Id)
        {
            var result = await _invoiceService.DownloadInvoiceAsync(Id);
            if (result.Succeeded)
            {
                var contract = result.Value;
                return File(contract.Data, contract.ContentType, contract.FileName);
            }
            throw new Exception(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadProForma(Guid Id)
        {
            var result = await _invoiceService.DownloadProFormaAsync(Id);
            if (result.Succeeded)
            {
                var contract = result.Value;
                return File(contract.Data, contract.ContentType, contract.FileName);
            }
            throw new Exception(result.ErrorMessage);
        }


        [HttpPost]
        public async Task<CommandResult> CustomerPassword([FromBody]GuidContract customerId)
        {
            return await _customerService.PasswordAsync(customerId.Id);
        }
        #endregion

    }
}