//-----------------------------------------------------------------------
// <copyright file="ServiceBaseFixture.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.Test.UnitTest.Fixtures
{
    using Microsoft.Extensions.Options;
    using Moq;
    using SEE.Framework.Core.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction;
    using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class ServiceBaseFixture
    {
        public Mock<IOptions<ApplicationConfiguration>> OptionsMock = new Mock<IOptions<ApplicationConfiguration>>();
        public Mock<IQuery> QueryMock = new Mock<IQuery>();
        public Mock<IStore> StoreMock = new Mock<IStore>();
        public Mock<ITransaction> TransactionMock = new Mock<ITransaction>();
    }
}
