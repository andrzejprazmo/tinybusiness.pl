//-----------------------------------------------------------------------
// <copyright file="ProductContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
	

    public class ProductContract
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int VatRateId { get; set; }
        public string VatRateName { get; set; }
        public decimal VatRateValue { get; set; }

        [DisplayFormat(DataFormatString = "0.00")]
        public decimal GrossPrice { get; set; }

        public string Version { get; set; }
    }
}
