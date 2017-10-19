using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
using SEE.Framework.Core.Common;
using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
using System.IO;
using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
using SEE.TinyBusinessMarket.BackEnd.Common.Extensions;

namespace SEE.TinyBusinessMarket.FrontEnd.Web.Controllers
{
    [Authorize(Roles = IdentityConsts.RoleCustomer)]
    public class CustomerController : Controller
    {
        #region members & ctor
        private readonly ICustomerService _customerService;
        private readonly IIdentityService _identityService;
        public CustomerController(IIdentityService identityService, ICustomerService customerService)
        {
            _identityService = identityService;
            _customerService = customerService;
        }
        #endregion

        [HttpPost]
        [AllowAnonymous]
        public async Task<CommandResult<CustomerUpdateModel>> Register([FromBody]CustomerUpdateModel model) => await _customerService.ValidateAndCreateAsync(model);

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string secret)
        {
            var result = await _identityService.LoginBySecretAsync(secret);
            if (result.Succeeded)
            {
                string redirect = Path.Combine(Request.PathBase + "/#/profile");
                return Redirect(redirect);
            }
            return RedirectToAction("BadToken", "Error");
        }

        [HttpGet]
        public async Task<ProfileContract> Details()
        {
            var customerId = User.GetCustomerId();
            if (customerId.HasValue)
            {
                return await _customerService.GetProfileAsync(customerId.Value);
            }
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<CommandResult> Password(string email)
        {
            return await _identityService.GenerateSecretAsync(email);
        }
    }
}