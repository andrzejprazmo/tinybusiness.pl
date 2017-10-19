//-----------------------------------------------------------------------
// <copyright file="PdfCreator.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using Microsoft.Extensions.Options;
    using System.Runtime.Loader;
    using System.Reflection;
    using System.IO;
    using SEE.TinyBusinessMarket.BackEnd.Common.Pdf;
    using Syncfusion.Pdf;
    using Syncfusion.Pdf.Graphics;
    using Syncfusion.Drawing;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;

    public class PdfCreator : IPdfCreator
    {
        #region member & ctor
        private const int marginSize = 25;
        private readonly ApplicationConfiguration configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PdfCreator(IOptions<ApplicationConfiguration> options, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = options.Value;
            _hostingEnvironment = hostingEnvironment;

            Init();
        }
        #endregion

        #region resources
        private PdfTrueTypeFont fontBig;
        private PdfTrueTypeFont fontNormal;
        private PdfTrueTypeFont fontNormalBold;
        private PdfTrueTypeFont fontNormalItalic;
        private PdfTrueTypeFont fontNormalItalicUnderline;
        private PdfTrueTypeFont fontSmall;
        private PdfTrueTypeFont fontSmallBold;
        private PdfTrueTypeFont fontSmallItalic;
        private PdfTrueTypeFont fontSmallItalicUnderline;
        private PdfSolidBrush brushBlack;
        private PdfPen penGray;
        #endregion

        private void Init()
        {
            string fontPath = Path.Combine(_hostingEnvironment.WebRootPath, "fonts");
            var fontRegularStream = File.OpenRead(Path.Combine(fontPath, "SourceSansPro-Regular.ttf"));
            var fontItalicStream = File.OpenRead(Path.Combine(fontPath, "SourceSansPro-Italic.ttf"));

            fontBig = new PdfTrueTypeFont(fontRegularStream, 14);

            fontNormal = new PdfTrueTypeFont(fontRegularStream, 12);
            fontNormalBold = new PdfTrueTypeFont(fontRegularStream, 12, PdfFontStyle.Bold);
            fontNormalItalic = new PdfTrueTypeFont(fontItalicStream, 12);
            fontNormalItalicUnderline = new PdfTrueTypeFont(fontItalicStream, 12, PdfFontStyle.Underline);

            fontSmall = new PdfTrueTypeFont(fontRegularStream, 10);
            fontSmallBold = new PdfTrueTypeFont(fontRegularStream, 10, PdfFontStyle.Bold);
            fontSmallItalic = new PdfTrueTypeFont(fontItalicStream, 10);
            fontSmallItalicUnderline = new PdfTrueTypeFont(fontItalicStream, 10, PdfFontStyle.Underline);

            brushBlack = new PdfSolidBrush(Color.FromArgb(255, 0, 0, 0));
            penGray = new PdfPen(Color.FromArgb(128, 128, 128), 1);

        }

        public byte[] ProForma(InvoicePdfModel model)
        {
            PdfLayoutResult layoutResult;
            string data;
            float y = 30;
            float x = 0;
            var document = new PdfDocument();
            document.PageSettings.Margins.All = marginSize;

            var page = document.Pages.Add();
            data = $"Faktura PRO FORMA nr {model.Number}";
            layoutResult = new PdfTextElement(data, fontBig, brushBlack).Draw(page, new PointF(x, y));

            data = $"Data wystawienia: {model.ExposureDate.ToString("yyyy-MM-dd")}";
            y = layoutResult.Bounds.Bottom;
            x = page.Size.Width - fontNormal.MeasureString(data).Width - marginSize * 2 - 4;
            layoutResult = new PdfTextElement("data wystawienia:", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x += layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.ExposureDate.ToString("yyyy-MM-dd"), fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            data = $"Miejsce wystawienia: {model.InvoiceConfig.ExposurePlace}";
            y = layoutResult.Bounds.Bottom;
            x = page.Size.Width - fontNormal.MeasureString(data).Width - marginSize * 2 - 4;
            layoutResult = new PdfTextElement("miejsce wystawienia:", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x += layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.InvoiceConfig.ExposurePlace, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            float section = layoutResult.Bounds.Bottom + 32;

            DrawBuyer(model, page, page.Size.Width / 2, section);

            section = DrawSeller(model, page, 0, section);

            section = section + 48;

            section = DrawProducts(model, page, section);

            section = section + 48;

            section = DrawFooter(model, page, 0, section);

            using (var memoryStream = new MemoryStream())
            {
                document.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public byte[] Invoice(InvoicePdfModel model)
        {
            PdfLayoutResult layoutResult;
            string data;
            float y = 30;
            float x = 0;
            var document = new PdfDocument();
            document.PageSettings.Margins.All = marginSize;

            var page = document.Pages.Add();
            data = $"Faktura VAT nr {model.Number} {model.InvoiceConfig.Additional}";
            layoutResult = new PdfTextElement(data, fontBig, brushBlack).Draw(page, new PointF(x, y));

            data = $"Data wystawienia: {model.ExposureDate.ToString("yyyy-MM-dd")}";
            y = layoutResult.Bounds.Bottom;
            x = page.Size.Width - fontNormal.MeasureString(data).Width - marginSize * 2 - 4;
            layoutResult = new PdfTextElement("data wystawienia:", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x += layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.ExposureDate.ToString("yyyy-MM-dd"), fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            data = $"Miejsce wystawienia: {model.InvoiceConfig.ExposurePlace}";
            y = layoutResult.Bounds.Bottom;
            x = page.Size.Width - fontNormal.MeasureString(data).Width - marginSize * 2 - 4;
            layoutResult = new PdfTextElement("miejsce wystawienia:", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x += layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.InvoiceConfig.ExposurePlace, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            data = $"Data sprzedaży: {model.SellDate.ToString("yyyy-MM-dd")}";
            y = layoutResult.Bounds.Bottom;
            x = page.Size.Width - fontNormal.MeasureString(data).Width - marginSize * 2 - 4;
            layoutResult = new PdfTextElement("data sprzedaży:", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x += layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.SellDate.ToString("yyyy-MM-dd"), fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            float section = layoutResult.Bounds.Bottom + 32;

            DrawBuyer(model, page, page.Size.Width / 2, section);

            section = DrawSeller(model, page, 0, section);

            section = section + 48;

            section = DrawProducts(model, page, section);

            section = section + 24;

            section = DrawRates(model, page, section);

            section = section + 48;

            section = DrawFooter(model, page, 0, section);

            y = section;
            x = 0;
            layoutResult = new PdfTextElement("płatność: ", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x = layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.PaymentTypeCaption, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            x = 0;
            layoutResult = new PdfTextElement("data zapłaty: ", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x = layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.SellDate.ToString("yyyy-MM-dd"), fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            using (var memoryStream = new MemoryStream())
            {
                document.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private float DrawBuyer(InvoicePdfModel model, PdfPage page, float x, float y)
        {
            PdfLayoutResult layoutResult;
            layoutResult = new PdfTextElement("nabywca:", fontNormalItalicUnderline, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            layoutResult = new PdfTextElement(model.Customer.Name, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            layoutResult = new PdfTextElement(model.Customer.StreetNumber, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            x = page.Size.Width / 2;
            layoutResult = new PdfTextElement(model.Customer.ZipCode, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));
            x += layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.Customer.City, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            x = page.Size.Width / 2;
            layoutResult = new PdfTextElement("NIP: ", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x += layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.Customer.Nip, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            return layoutResult.Bounds.Bottom;
        }

        private float DrawSeller(InvoicePdfModel model, PdfPage page, float x, float y)
        {
            PdfLayoutResult layoutResult;
            layoutResult = new PdfTextElement("sprzedawca:", fontNormalItalicUnderline, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            layoutResult = new PdfTextElement(model.InvoiceConfig.Seller.Name, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            layoutResult = new PdfTextElement("NIP: ", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x = layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.InvoiceConfig.Seller.Nip, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            x = 0;
            layoutResult = new PdfTextElement(model.InvoiceConfig.Seller.StreetNumber, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            x = 0;
            layoutResult = new PdfTextElement(model.InvoiceConfig.Seller.ZipCode, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));
            x = layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.InvoiceConfig.Seller.City, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            x = 0;
            layoutResult = new PdfTextElement("nr konta: ", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x = layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.InvoiceConfig.Seller.AccountNumber, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            x = 0;
            layoutResult = new PdfTextElement(model.InvoiceConfig.Seller.Bank, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            return layoutResult.Bounds.Bottom;
        }

        private float DrawProducts(InvoicePdfModel model, PdfPage page, float y)
        {
            PdfLayoutResult layoutResult;
            float xCol1 = 2;
            float xCol2 = 16;
            float xCol3 = xCol2 + 140;
            float xCol4 = xCol3 + 40;
            float xCol5 = xCol4 + 30;
            float xCol6 = xCol5 + 30;
            float xCol7 = xCol6 + 50;
            float xCol8 = xCol7 + 60;
            float xCol9 = xCol8 + 60;
            float xCol10 = xCol9 + 50;

            float padding = 2;
            float height = 0;

            layoutResult = new PdfTextElement("#", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol1, y));
            height = layoutResult.Bounds.Height + padding * 2;
            page.Graphics.DrawRectangle(penGray, xCol1 - padding, y - padding, xCol2 - xCol1, height);
            layoutResult = new PdfTextElement("Nazwa", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol2, y));
            page.Graphics.DrawRectangle(penGray, xCol2 - padding, y - padding, xCol3 - xCol2, height);
            layoutResult = new PdfTextElement("PKWiU", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol3, y));
            page.Graphics.DrawRectangle(penGray, xCol3 - padding, y - padding, xCol4 - xCol3, height);
            layoutResult = new PdfTextElement("Ilość", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol4, y));
            page.Graphics.DrawRectangle(penGray, xCol4 - padding, y - padding, xCol5 - xCol4, height);
            layoutResult = new PdfTextElement("Jm.", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol5, y));
            page.Graphics.DrawRectangle(penGray, xCol5 - padding, y - padding, xCol6 - xCol5, height);
            layoutResult = new PdfTextElement("Cena jedn.", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol6, y));
            page.Graphics.DrawRectangle(penGray, xCol6 - padding, y - padding, xCol7 - xCol6, height);
            layoutResult = new PdfTextElement("Stawka VAT", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol7, y));
            page.Graphics.DrawRectangle(penGray, xCol7 - padding, y - padding, xCol8 - xCol7, height);
            layoutResult = new PdfTextElement("Wartość netto", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol8, y));
            page.Graphics.DrawRectangle(penGray, xCol8 - padding, y - padding, xCol9 - xCol8, height);
            layoutResult = new PdfTextElement("Kwota VAT", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol9, y));
            page.Graphics.DrawRectangle(penGray, xCol9 - padding, y - padding, xCol10 - xCol9, height);
            layoutResult = new PdfTextElement("Wartość brutto", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol10, y));
            page.Graphics.DrawRectangle(penGray, xCol10 - padding, y - padding, page.Size.Width - marginSize * 2 - xCol10, height);

            float section = layoutResult.Bounds.Bottom + padding * 2;
            int counter = 0;
            y = section;
            foreach (var product in model.Products)
            {
                layoutResult = new PdfTextElement($"{(++counter).ToString()}.", fontSmallBold, brushBlack).Draw(page, new PointF(xCol1, y));
                height = layoutResult.Bounds.Height + padding * 2;
                page.Graphics.DrawRectangle(penGray, xCol1 - padding, y - padding, xCol2 - xCol1, height);
                layoutResult = new PdfTextElement(product.Name, fontSmallBold, brushBlack).Draw(page, new PointF(xCol2, y));
                page.Graphics.DrawRectangle(penGray, xCol2 - padding, y - padding, xCol3 - xCol2, height);
                layoutResult = new PdfTextElement(string.Empty, fontSmallBold, brushBlack).Draw(page, new PointF(xCol3, y));
                page.Graphics.DrawRectangle(penGray, xCol3 - padding, y - padding, xCol4 - xCol3, height);
                layoutResult = new PdfTextElement(product.Quantity.ToString("0"), fontSmallBold, brushBlack).Draw(page, new PointF(xCol4, y));
                page.Graphics.DrawRectangle(penGray, xCol4 - padding, y - padding, xCol5 - xCol4, height);
                layoutResult = new PdfTextElement(product.MeasureUnitCaption, fontSmallBold, brushBlack).Draw(page, new PointF(xCol5, y));
                page.Graphics.DrawRectangle(penGray, xCol5 - padding, y - padding, xCol6 - xCol5, height);
                layoutResult = new PdfTextElement($"{product.UnitPrice.ToString("#,##0.00")} zł", fontSmallBold, brushBlack).Draw(page, new PointF(xCol6, y));
                page.Graphics.DrawRectangle(penGray, xCol6 - padding, y - padding, xCol7 - xCol6, height);
                layoutResult = new PdfTextElement(product.VatRateCaption, fontSmallBold, brushBlack).Draw(page, new PointF(xCol7, y));
                page.Graphics.DrawRectangle(penGray, xCol7 - padding, y - padding, xCol8 - xCol7, height);
                layoutResult = new PdfTextElement($"{product.NetAmount.ToString("#,##0.00")} zł", fontSmallBold, brushBlack).Draw(page, new PointF(xCol8, y));
                page.Graphics.DrawRectangle(penGray, xCol8 - padding, y - padding, xCol9 - xCol8, height);
                layoutResult = new PdfTextElement($"{product.VatAmount.ToString("#,##0.00")} zł", fontSmallBold, brushBlack).Draw(page, new PointF(xCol9, y));
                page.Graphics.DrawRectangle(penGray, xCol9 - padding, y - padding, xCol10 - xCol9, height);
                layoutResult = new PdfTextElement($"{product.GrossAmount.ToString("#,##0.00")} zł", fontSmallBold, brushBlack).Draw(page, new PointF(xCol10, y));
                page.Graphics.DrawRectangle(penGray, xCol10 - padding, y - padding, page.Size.Width - marginSize * 2 - xCol10, height);
                y = layoutResult.Bounds.Bottom;
            }
            return layoutResult.Bounds.Bottom;
        }

        private float DrawRates(InvoicePdfModel model, PdfPage page, float y)
        {
            PdfLayoutResult layoutResult;
            layoutResult = new PdfTextElement("ogółem stawkami:", fontSmallItalic, brushBlack).Draw(page, new PointF(0, y));
            y = layoutResult.Bounds.Bottom + 4;

            float xCol1 = 2;
            float xCol2 = xCol1 + 60;
            float xCol3 = xCol2 + 60;
            float xCol4 = xCol3 + 60;
            float xCol5 = xCol4 + 80;

            float padding = 2;
            float height = 0;

            layoutResult = new PdfTextElement("Stawka VAT", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol1, y));
            height = layoutResult.Bounds.Height + padding * 2;
            page.Graphics.DrawRectangle(penGray, xCol1 - padding, y - padding, xCol2 - xCol1, height);
            layoutResult = new PdfTextElement("Wartość netto", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol2, y));
            page.Graphics.DrawRectangle(penGray, xCol2 - padding, y - padding, xCol3 - xCol2, height);
            layoutResult = new PdfTextElement("Kwota VAT", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol3, y));
            page.Graphics.DrawRectangle(penGray, xCol3 - padding, y - padding, xCol4 - xCol3, height);
            layoutResult = new PdfTextElement("Wartość brutto", fontSmallItalic, brushBlack).Draw(page, new PointF(xCol4, y));
            page.Graphics.DrawRectangle(penGray, xCol4 - padding, y - padding, xCol5 - xCol4, height);
            y = layoutResult.Bounds.Bottom + padding * 2;

            foreach (var tax in model.Rates)
            {
                layoutResult = new PdfTextElement(tax.VatRateCaption, fontSmallBold, brushBlack).Draw(page, new PointF(xCol1, y));
                height = layoutResult.Bounds.Height + padding * 2;
                page.Graphics.DrawRectangle(penGray, xCol1 - padding, y - padding, xCol2 - xCol1, height);
                layoutResult = new PdfTextElement($"{tax.NetAmount.ToString("#,##0.00")} zł", fontSmallBold, brushBlack).Draw(page, new PointF(xCol2, y));
                page.Graphics.DrawRectangle(penGray, xCol2 - padding, y - padding, xCol3 - xCol2, height);
                layoutResult = new PdfTextElement($"{tax.VatAmount.ToString("#,##0.00")} zł", fontSmallBold, brushBlack).Draw(page, new PointF(xCol3, y));
                page.Graphics.DrawRectangle(penGray, xCol3 - padding, y - padding, xCol4 - xCol3, height);
                layoutResult = new PdfTextElement($"{tax.GrossAmount.ToString("#,##0.00")} zł", fontSmallBold, brushBlack).Draw(page, new PointF(xCol4, y));
                page.Graphics.DrawRectangle(penGray, xCol4 - padding, y - padding, xCol5 - xCol4, height);
                y = layoutResult.Bounds.Bottom;
            }
            return layoutResult.Bounds.Bottom;
        }

        private float DrawFooter(InvoicePdfModel model, PdfPage page, float x, float y)
        {
            PdfLayoutResult layoutResult;
            layoutResult = new PdfTextElement("do zapłaty: ", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x = layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement($"{model.GrossAmount.ToString("#,##0.00")} zł", fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            y = layoutResult.Bounds.Bottom;
            x = 0;
            layoutResult = new PdfTextElement("słownie: ", fontNormalItalic, brushBlack).Draw(page, new PointF(x, y));
            x = layoutResult.Bounds.Width + 4;
            layoutResult = new PdfTextElement(model.GrossAmountStringified, fontNormalBold, brushBlack).Draw(page, new PointF(x, y));

            return layoutResult.Bounds.Bottom;

        }
    }
}

