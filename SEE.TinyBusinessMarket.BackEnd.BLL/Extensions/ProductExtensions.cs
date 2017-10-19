//-----------------------------------------------------------------------
// <copyright file="ProductExtensions.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;

    public static class ProductExtensions
    {
        public static IQueryable<ProductContract> SelectProductContract(this IQueryable<ProductEntity> query)
        {
            return query.Select(x => new ProductContract
            {
                Id = x.Id,
                Name = x.Name,
                FullName = x.FullName,
                UnitPrice = x.UnitPrice,
                VatRateId = x.VatRateId,
                VatRateName = x.VatRate.Name,
                VatRateValue = x.VatRate.Value,
                Version = x.Version,
                GrossPrice = x.UnitPrice + (x.UnitPrice * (x.VatRate.Value / 100))
            });
        }
    }
}
