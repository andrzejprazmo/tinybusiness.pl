//-----------------------------------------------------------------------
// <copyright file="TransactionStatusEnum.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Enum
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public enum OrderStatusEnum
    {
        INIT = 1,
        PENDING = 2,
        COMPLETED = 3,
        CANCELED = 4,
        REJECTED = 5,
    }
}
