//-----------------------------------------------------------------------
// <copyright file="InvoiceConfiguration.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class InvoiceConfiguration
    {
        public string Additional { get; set; }
        public string ExposurePlace { get; set; }
        public int PaymentDays { get; set; }

        public SellerConfiguration Seller { get; set; }
    }
}
