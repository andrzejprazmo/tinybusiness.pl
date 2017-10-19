//-----------------------------------------------------------------------
// <copyright file="InvoiceExtensions.cs" company="SEE Software">
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


    public static class InvoiceExtensions
    {
        public static IQueryable<InvoiceContract> SelectInvoiceContract(this IQueryable<InvoiceEntity> query) => query.Select(x => new InvoiceContract
        {
            LicenceId = x.LicenceId,
            ExposureDate = x.ExposureDate,
            Number = x.Number,
            PaymentDate = x.PaymentDate,
            PaymentDays = x.PaymentDays,
            PaymentTypeId = x.PaymentTypeId,
            PaymentTypeCaption = x.PaymentType.Name,
            PaymentTypeCode = x.PaymentType.Code,
            SellDate = x.SellDate,
        });
    }
}
