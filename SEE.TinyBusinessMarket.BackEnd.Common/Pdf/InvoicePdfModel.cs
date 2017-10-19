//-----------------------------------------------------------------------
// <copyright file="InvoicePdfModel.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Pdf
{
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public class InvoicePdfModel
    {
        public InvoiceConfiguration InvoiceConfig { get; set; }

        public string Number { get; set; }
        public DateTime ExposureDate { get; set; }
        public DateTime SellDate { get; set; }

        public CustomerContract Customer { get; set; }

        public decimal GrossAmount
        {
            get => Products.Sum(x => x.GrossAmount);
        }
        public string GrossAmountStringified { get; set; }
        public string PaymentTypeCaption { get; set; }
        public string PaymentTypeCode { get; set; }
        public int PaymentDays { get; set; }
        public DateTime ExpectedPaymentDate
        {
            get => SellDate.AddDays(PaymentDays);
        }

        public List<InvoiceProductPdfModel> Products { get; set; }
        public List<InvoiceVatRatePdfModel> Rates { get; set; }
    }

    public class InvoiceProductPdfModel
    {
        public string Name { get; set; }
        public string Pkwiu { get; set; }
        public decimal Quantity { get; set; }
        public string MeasureUnitCaption { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VatRateValue { get; set; }
        public string VatRateCaption { get; set; }
        public decimal NetAmount
        {
            get => UnitPrice * Quantity;
        }
        public decimal VatAmount
        {
            get => NetAmount * (VatRateValue / 100);
        }
        public decimal GrossAmount
        {
            get => NetAmount + VatAmount;
        }
    }

    public class InvoiceVatRatePdfModel
    {
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal VatRateValue { get; set; }
        public string VatRateCaption { get; set; }
        public decimal NetAmount
        {
            get => UnitPrice * Quantity;
        }
        public decimal VatAmount
        {
            get => NetAmount * (VatRateValue / 100);
        }
        public decimal GrossAmount
        {
            get => NetAmount + VatAmount;
        }

    }
}
