//-----------------------------------------------------------------------
// <copyright file="CustomerServiceUnitTest.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.Test.UnitTest
{
    using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using SEE.TinyBusinessMarket.Test.UnitTest.Fixtures;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using SEE.Framework.Core.DTO;
    using SEE.TinyBusinessMarket.Test.UnitTest.Stub;
    

    public class CustomerServiceUnitTest
    {
        [Fact]
        public async Task CustomerService_Register_InputHasNoData_ResultFail()
        {
            // Arrange
            var fixture = new CustomerServiceFixture();
            var model = new CustomerUpdateModel();
            fixture.QueryMock.Setup(m => m.Get<CustomerEntity>()).Returns(new List<CustomerEntity>().AsQueryable());

            // Act
            var sut = fixture.CreateSut();
            var result = await sut.ValidateAndCreateAsync(model);

            // Assert
            Assert.False(result.Succeeded);
            fixture.QueryMock.Verify(m => m.Get<CustomerEntity>(), Times.Never()); // Because email is empty
        }

        [Fact]
        public async Task CustomerService_Register_InputHasValidEmailOnly_ResultFail()
        {
            // Arrange
            var fixture = new CustomerServiceFixture();
            var model = new CustomerUpdateModel
            {
                Email = new Data<string>("test@domain.com"),
            };
            fixture.QueryMock.Setup(m => m.Get<CustomerEntity>()).Returns(new List<CustomerEntity>().AsQueryable());

            // Act
            var sut = fixture.CreateSut();
            var result = await sut.ValidateAndCreateAsync(model);

            // Assert
            Assert.False(result.Succeeded);
            fixture.QueryMock.Verify(m => m.Get<CustomerEntity>(), Times.Once());
        }

        [Fact]
        public async Task CustomerService_Register_InputIsValidEmailExists_ResultFail()
        {
            // Arrange
            var fixture = new CustomerServiceFixture();
            var model = new CustomerUpdateModel
            {
                FirstName = new Data<string>("John"),
                LastName = new Data<string>("Doe"),
                Email = new Data<string>("test@domain.com"),
                City = new Data<string>("City"),
                Name = new Data<string>("CustomerName"),
                Nip = new Data<string>("7131340237"),
                Phone = new Data<string>("111222333"),
                StreetNumber = new Data<string>("StreetNumber"),
                ZipCode = new Data<string>("00-000"),

            };

            var existingCustomers = new List<CustomerEntity>
            {
                new CustomerEntity
                {
                    Email = "test@domain.com",
                }
            }.AsQueryable();

            fixture.QueryMock.Setup(m => m.Get<CustomerEntity>()).Returns(existingCustomers);

            // Act
            var sut = fixture.CreateSut();
            var result = await sut.ValidateAndCreateAsync(model);

            // Assert
            Assert.False(result.Succeeded);
            Assert.True(result.Value.Email.HasError);
            fixture.QueryMock.Verify(m => m.Get<CustomerEntity>(), Times.Once());
        }
    }
}
