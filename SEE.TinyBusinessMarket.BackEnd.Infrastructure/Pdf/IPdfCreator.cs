//-----------------------------------------------------------------------
// <copyright file="IPdfCreator.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Pdf
{
    using SEE.TinyBusinessMarket.BackEnd.Common.Pdf;
    using System;
	using System.Collections.Generic;
	using System.Text;
	

    public interface IPdfCreator
    {
        byte[] ProForma(InvoicePdfModel model);
        byte[] Invoice(InvoicePdfModel model);
    }
}
