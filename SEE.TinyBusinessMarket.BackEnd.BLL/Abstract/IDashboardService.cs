//-----------------------------------------------------------------------
// <copyright file="IDashboardService.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Abstract
{
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public interface IDashboardService
    {
        Task<DashboardContract> DetailsAsync();
    }
}
