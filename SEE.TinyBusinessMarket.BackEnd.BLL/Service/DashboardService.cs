//-----------------------------------------------------------------------
// <copyright file="DashboardService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Service
{
    using SEE.TinyBusinessMarket.BackEnd.BLL.Base;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using SEE.Framework.Core.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;
    using SEE.Framework.Core.DTO;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System.Linq;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Extensions;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using SEE.TinyBusinessMarket.BackEnd.Common.Enum;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using Microsoft.Extensions.Options;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class DashboardService : ServiceBase<DashboardService>, IDashboardService
    {
        public DashboardService(ILog<DashboardService> log, IOptions<ApplicationConfiguration> options, IQuery query, IStore store, ITransaction transaction) : base(log, options, query, store, transaction)
        {
        }

        public async Task<DashboardContract> DetailsAsync()
        {
            return new DashboardContract
            {
                Product = await query.Get<ProductEntity>().Where(x => x.Active).SelectProductContract().SingleAsync(),
            };
        }
    }
}
