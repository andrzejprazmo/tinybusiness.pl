//-----------------------------------------------------------------------
// <copyright file="InvoiceContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class InvoiceContract
    {
        public Guid LicenceId { get; set; }
        public string Number { get; set; }
        public DateTime ExposureDate { get; set; }
        public DateTime SellDate { get; set; }
        public int PaymentDays { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeCode { get; set; }
        public string PaymentTypeCaption { get; set; }
    }


}
