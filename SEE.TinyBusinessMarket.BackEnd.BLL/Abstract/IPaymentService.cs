//-----------------------------------------------------------------------
// <copyright file="IPaymentService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Abstract
{
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.UpdateModel;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IPaymentService
    {
        Task<PaymentUpdateModel> AddAsync(Guid productId);
        Task<CommandResult<PaymentUpdateModel>> StartOrderAsync(PaymentUpdateModel model);
        Task<CommandResult> FinishOrderAsync(string token);
        Task<CommandResult<FinishOrderUpdateModel>> FinishOrderAsync(FinishOrderUpdateModel model);
        Task<CommandResult<PaymentUpdateModel>> RemitanceAsync(PaymentUpdateModel model);
        Task<CommandResult> SendProFormaAsync(Guid proFormaId);
    }
}
