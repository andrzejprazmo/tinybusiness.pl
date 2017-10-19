using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;

namespace SEE.TinyBusinessMarket.FrontEnd.Web.Controllers
{
    public class ProductController : Controller
    {
        #region members & ctor
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        [HttpGet]
        public async Task<ProductContract> Details(Guid productId) => await _productService.DetailsAsync(productId);

    }
}