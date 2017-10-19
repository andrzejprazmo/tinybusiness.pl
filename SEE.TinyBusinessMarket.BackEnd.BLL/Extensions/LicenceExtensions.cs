//-----------------------------------------------------------------------
// <copyright file="LicenceExtensions.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Extensions
{
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public static class LicenceExtensions
    {
        public static IQueryable<LicenceContract> SelectLicenceContract(this IQueryable<LicenceEntity> query) => query.Select(x => new LicenceContract
        {
            Id = x.Id,
            ValidFrom = x.ValidFrom,
            ValidTo = x.ValidTo,
            Quantity = x.Quantity,
            ProductId = x.ProductId,
            ProductFullName = x.Product.FullName,
            ProductUnitPrice = x.Product.UnitPrice,
            ProductVatRateId = x.Product.VatRateId,
            ProductVatRateName = x.Product.VatRate.Name,
            ProductVatRateValue = x.Product.VatRate.Value,
            ProductGrossPrice = x.Product.UnitPrice + (x.Product.UnitPrice * (x.Product.VatRate.Value / 100)),
            CustomerFirstName = x.Customer.FirstName,
            CustomerLastName = x.Customer.LastName,
            CustomerCity = x.Customer.City,
            CustomerEmail = x.Customer.Email,
            CustomerId = x.CustomerId,
            CustomerName = x.Customer.Name,
            CustomerNip = x.Customer.Nip,
            CustomerStreetNumber = x.Customer.StreetNumber,
            CustomerZipCode = x.Customer.ZipCode,
        });
    }
}
