using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
using SEE.TinyBusinessMarket.BackEnd.Common.Contract;

namespace SEE.TinyBusinessMarket.FrontEnd.Web.Controllers
{
    public class DashboardController : Controller
    {
        #region members & ctor
        private readonly IDashboardService _dasboardService;
        public DashboardController(IDashboardService dasboardService)
        {
            _dasboardService = dasboardService;
        }
        #endregion

        [HttpGet]
        public async Task<DashboardContract> Details() => await _dasboardService.DetailsAsync();
    }
}