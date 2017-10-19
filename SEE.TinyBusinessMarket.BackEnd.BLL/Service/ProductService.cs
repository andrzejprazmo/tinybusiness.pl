//-----------------------------------------------------------------------
// <copyright file="ProductService.cs" company="SEE Software">
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
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using SEE.Framework.Core.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using System.Threading.Tasks;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System.Linq;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Extensions;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : ServiceBase<ProductService>, IProductService
    {
        public ProductService(ILog<ProductService> log, IOptions<ApplicationConfiguration> options, IQuery query, IStore store, ITransaction transactionScope) 
            : base(log, options, query, store, transactionScope)
        {
        }

        public async Task<ProductContract> DetailsAsync(Guid productId) => await query.Get<ProductEntity>().Where(x => x.Id == productId).SelectProductContract().SingleAsync();
    }
}
