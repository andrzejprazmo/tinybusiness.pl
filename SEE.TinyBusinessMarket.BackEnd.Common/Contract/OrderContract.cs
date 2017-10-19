//-----------------------------------------------------------------------
// <copyright file="OrderContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class OrderContract
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finish { get; set; }
        public string PayuOrderId { get; set; }
        public string PayuExtOrderId { get; set; }
        public string Token { get; set; }
        public int OrderTypeId { get; set; }
        public int Quantity { get; set; }

        public string ProductName { get; set; }
        public string ProductFullName { get; set; }
        public string OrderTypeName { get; set; }
        public string OrderStatusName { get; set; }
        public string OrderTypeCode { get; set; }
        public string OrderStatusCode { get; set; }

    }
}
