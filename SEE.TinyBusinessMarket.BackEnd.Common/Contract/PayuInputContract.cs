//-----------------------------------------------------------------------
// <copyright file="PayuInputContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
	using System.Text;


    public class PayuInputContract
    {

        public string NotifyUrl { get; set; }
        public string CustomerIp { get; set; }
        public string MerchantPosId { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public int TotalAmount { get; set; }
        public PayuBuyerContract Buyer { get; set; }
        public PayuSettingsContract Settings { get; set; }
        public List<PayuProductContract> Products { get; set; }
    }

    public class PayuBuyerContract
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Language { get; set; }
    }

    public class PayuSettingsContract
    {
        public bool InvoiceDisabled { get; set; }
    }

    public class PayuProductContract
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
