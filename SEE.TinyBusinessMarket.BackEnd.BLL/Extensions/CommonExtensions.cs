//-----------------------------------------------------------------------
// <copyright file="CommonExtensions.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using SEE.Framework.Core.Collection;
    using System;
	using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class CommonExtensions
    {
        public async static Task<DataResponse<TRequest, TResponse>> ToDataResponseAsync<TRequest, TResponse>(this IQueryable<TResponse> query, TRequest request)
            where TResponse : class
            where TRequest : DataRequest
        {
            request.TotalRecords = query.Count();
            if (request.PageSize == -1)
            {
                return new DataResponse<TRequest, TResponse>(request, await query.ToListAsync());
            }
            var list = await query.Skip(request.Offset).Take(request.PageSize).ToListAsync();
            return new DataResponse<TRequest, TResponse>(request, list);
        }
    }
}
