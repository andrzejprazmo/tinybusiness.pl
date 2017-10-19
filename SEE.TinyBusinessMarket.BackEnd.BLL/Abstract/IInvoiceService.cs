//-----------------------------------------------------------------------
// <copyright file="IInvoiceService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Abstract
{
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.Base;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Pdf;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IInvoiceService
    {
        Task<CommandResult<DownloadContract>> DownloadInvoiceAsync(Guid invoiceId);
        Task<CommandResult<DownloadContract>> DownloadInvoiceAsync(string token, Guid invoiceId);
        Task<CommandResult<DownloadContract>> DownloadProFormaAsync(Guid orderId);
    }
}
