//-----------------------------------------------------------------------
// <copyright file="SellerConfiguration.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class SellerConfiguration
    {
        public string Name { get; set; }
        public string Nip { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
    }
}
