//-----------------------------------------------------------------------
// <copyright file="PayuConfiguration.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class PayuConfiguration
    {
        public string OrderUrl { get; set; }
        public string AuthorizeUrl { get; set; }
        public string ClientSecret { get; set; }
        public string PosId { get; set; }
    }
}
