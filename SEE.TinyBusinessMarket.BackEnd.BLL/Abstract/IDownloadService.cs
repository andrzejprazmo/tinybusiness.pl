//-----------------------------------------------------------------------
// <copyright file="IDownloadService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Abstract
{
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.Base;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IDownloadService
    {
        Task<CommandResult<DownloadContract>> DownloadDemoAsync(Guid productId);
        Task<CommandResult<DownloadContract>> DownloadFullAsync(Guid customerId, Guid productId);
        Task<CommandResult<DownloadContract>> DownloadInvoiceAsync(Guid customerId, Guid invoiceId);
    }
}
