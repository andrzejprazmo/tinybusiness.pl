//-----------------------------------------------------------------------
// <copyright file="CustomerServiceFixture.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.Test.UnitTest.Fixtures
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using Moq;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.BLL.Service;
    using Microsoft.Extensions.Options;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using SEE.Framework.Core.Abstract;

    public class CustomerServiceFixture : ServiceBaseFixture
    {
        public Mock<ILog<CustomerService>> LogMock = new Mock<ILog<CustomerService>>();
        public Mock<IMailService> MailServiceMock = new Mock<IMailService>();

        public ICustomerService CreateSut()
        {
            return new CustomerService(LogMock.Object, OptionsMock.Object, QueryMock.Object, StoreMock.Object, TransactionMock.Object, MailServiceMock.Object);
        }
    }
}
