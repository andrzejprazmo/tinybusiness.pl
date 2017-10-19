//-----------------------------------------------------------------------
// <copyright file="IPayuRepository.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Payu
{
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IPayuManager
    {
        Task<CommandResult<PayuOutputContract>> CreateOrderAsync(string token, PayuProductContract product, PayuBuyerContract buyer);
    }
}
