//-----------------------------------------------------------------------
// <copyright file="IIdentityService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Abstract
{
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<CommandResult> LoginBySecretAsync(string secret);
        Task<CommandResult> GenerateSecretAsync(string emailAddress);
        Task<CommandResult> LoginAsync(LoginContract model);
    }
}
