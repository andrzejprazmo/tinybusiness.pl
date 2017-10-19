//-----------------------------------------------------------------------
// <copyright file="DownloadService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Service
{
    using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Base;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using SEE.Framework.Core.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.File;
    using SEE.TinyBusinessMarket.BackEnd.Common.Base;
    using SEE.Framework.Core.Common;
    using System.IO;
    using System.Threading.Tasks;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System.Linq;

    public class DownloadService : ServiceBase<DownloadService>, IDownloadService
    {
        #region members & ctor
        private readonly IFileRepository _fileRepository;
        private readonly IInvoiceService _invoiceService;
        public DownloadService(ILog<DownloadService> log
            , IOptions<ApplicationConfiguration> options
            , IQuery query
            , IStore store
            , ITransaction transactionScope
            , IFileRepository fileRepository
            , IInvoiceService invoiceService
            )
            : base(log, options, query, store, transactionScope)
        {
            _fileRepository = fileRepository;
            _invoiceService = invoiceService;
        }

        #endregion

        public async Task<CommandResult<DownloadContract>> DownloadDemoAsync(Guid productId)
        {
            try
            {
                string path = Path.Combine(configuration.DownloadConfiguration.DemoFolder, productId.ToString());
                if (_fileRepository.Exists(path))
                {
                    var downloadContract = new DownloadContract
                    {
                        ContentType = "application/zip",
                        FileName = "TinyBusinessDemo.zip",
                        Data = await _fileRepository.GetAsync(path),
                    };
                    return new CommandResult<DownloadContract>(downloadContract);
                }
                return new CommandResult<DownloadContract>(null, Localization.Resource.Download_FileNotExists);
            }
            catch (Exception e)
            {
                log.Error(nameof(DownloadDemoAsync), e);
                return new CommandResult<DownloadContract>(null, e);
            }
        }

        public async Task<CommandResult<DownloadContract>> DownloadFullAsync(Guid customerId, Guid productId)
        {
            try
            {
                var now = DateTime.Now;
                if (query.Get<LicenceEntity>().Any(x => x.ProductId == productId && x.CustomerId == customerId && x.ValidFrom <= now && now <= x.ValidTo))
                {
                    string path = Path.Combine(configuration.DownloadConfiguration.ProdFolder, productId.ToString());
                    if (_fileRepository.Exists(path))
                    {
                        var downloadContract = new DownloadContract
                        {
                            ContentType = "application/zip",
                            FileName = "TinyBusiness.zip",
                            Data = await _fileRepository.GetAsync(path),
                        };
                        return new CommandResult<DownloadContract>(downloadContract);
                    }
                }
                return new CommandResult<DownloadContract>(null, Localization.Resource.Download_FileNotExists);
            }
            catch (Exception e)
            {
                log.Error(nameof(DownloadFullAsync), e);
                return new CommandResult<DownloadContract>(null, e);
            }
        }

        public async Task<CommandResult<DownloadContract>> DownloadInvoiceAsync(Guid customerId, Guid invoiceId)
        {
            try
            {
                if (query.Get<InvoiceEntity>().Any(x => x.LicenceId == invoiceId && x.Licence.CustomerId == customerId))
                {
                    return await _invoiceService.DownloadInvoiceAsync(invoiceId);
                }
                return new CommandResult<DownloadContract>(null, Localization.Resource.Download_FileNotExists);
            }
            catch (Exception e)
            {
                log.Error(nameof(DownloadInvoiceAsync), e);
                return new CommandResult<DownloadContract>(null, e);
            }
        }


    }
}
