using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
using Microsoft.AspNetCore.Authorization;
using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
using SEE.TinyBusinessMarket.BackEnd.Common.Extensions;

namespace SEE.TinyBusinessMarket.FrontEnd.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IDownloadService _downloadService;
        public DownloadController(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }

        [HttpGet]
        public async Task<IActionResult> Demo(Guid id)
        {
            var result =  await _downloadService.DownloadDemoAsync(id);
            if (result.Succeeded)
            {
                var contract = result.Value;
                return File(contract.Data, contract.ContentType, contract.FileName);
            }
            return View("~/Views/Error/DownloadError.cshtml");
        }

        [HttpGet]
        [Authorize(Roles = IdentityConsts.RoleCustomer)]
        public async Task<IActionResult> Invoice(Guid id)
        {
            var customerId = User.GetCustomerId();
            if (customerId.HasValue)
            {
                var result = await _downloadService.DownloadInvoiceAsync(customerId.Value, id);
                if (result.Succeeded)
                {
                    var contract = result.Value;
                    return File(contract.Data, contract.ContentType, contract.FileName);
                }
            }
            return View("~/Views/Error/DownloadError.cshtml");
        }

        [HttpGet]
        [Authorize(Roles = IdentityConsts.RoleCustomer)]
        public async Task<IActionResult> Product(Guid id)
        {
            var customerId = User.GetCustomerId();
            if (customerId.HasValue)
            {
                var result = await _downloadService.DownloadFullAsync(customerId.Value, id);
                if (result.Succeeded)
                {
                    var contract = result.Value;
                    return File(contract.Data, contract.ContentType, contract.FileName);
                }
            }
            return View("~/Views/Error/DownloadError.cshtml");
        }

    }
}