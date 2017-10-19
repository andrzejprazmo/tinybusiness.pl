//-----------------------------------------------------------------------
// <copyright file="LicenceContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class LicenceContract
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int Quantity { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNip { get; set; }
        public string CustomerStreetNumber { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerCity { get; set; }
        public string ProductFullName { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public int ProductVatRateId { get; set; }
        public string ProductVatRateName { get; set; }
        public decimal ProductVatRateValue { get; set; }
        public decimal ProductGrossPrice { get; set; }

    }
}
