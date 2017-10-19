//-----------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Base
{
    using SEE.Framework.Core.Abstract;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using Microsoft.Extensions.Logging;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using Microsoft.Extensions.Options;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;

    public class ServiceBase<TService>
    {
        protected readonly ILog<TService> log;
        protected readonly IQuery query;
        protected readonly IStore store;
        protected readonly ITransaction transactionScope;
        protected readonly ApplicationConfiguration configuration;

        public ServiceBase(ILog<TService> log, IOptions<ApplicationConfiguration> options, IQuery query, IStore store, ITransaction transactionScope)
        {
            this.log = log;
            this.query = query;
            this.store = store;
            this.transactionScope = transactionScope;
            this.configuration = options.Value;
        }
    }
}
