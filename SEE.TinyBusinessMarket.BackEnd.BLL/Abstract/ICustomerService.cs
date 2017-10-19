//-----------------------------------------------------------------------
// <copyright file="ICustomerService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Abstract
{
    using SEE.Framework.Core.Collection;
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Request;
    using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface ICustomerService
    {
        Task<bool> Validate(CustomerUpdateModel model);
        Task<CustomerEntity> CreateAsync(CustomerUpdateModel model);
        Task<CommandResult<CustomerUpdateModel>> ValidateAndCreateAsync(CustomerUpdateModel model);
        Task<ProfileContract> GetProfileAsync(Guid customerId);
        Task<CustomerContract> GetDetailsAsync(Guid customerId);
        Task<DataResponse<CustomerRequest, CustomerContract>> FindBy(CustomerRequest request);
        Task<IReadOnlyList<OrderContract>> Orders(Guid customerId);
        Task<IReadOnlyList<LicenceContract>> Licences(Guid customerId);
        Task<CommandResult> PasswordAsync(Guid customerId);
    }
}
