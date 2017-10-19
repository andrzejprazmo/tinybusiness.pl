//-----------------------------------------------------------------------
// <copyright file="IMailService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Abstract
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IMailService
    {
        Task SendDownloadTokenAsync(Guid tokenId);
        Task SendInvoiceAsync(Guid invoiceId);
        Task SendTokenAsync(Guid tokenId);
        Task SendProFormaAsync(Guid orderId);
    }
}
