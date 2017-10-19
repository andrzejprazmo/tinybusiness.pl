//-----------------------------------------------------------------------
// <copyright file="PayuOutputContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class PayuOutputContract
    {
        public string RedirectUri { get; set; }
        public string OrderId { get; set; }
        public string ExtOrderId { get; set; }
    }
}
