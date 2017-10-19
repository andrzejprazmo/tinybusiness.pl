//-----------------------------------------------------------------------
// <copyright file="AsyncExtensions.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.Test.UnitTest.Stub
{
	using System;
	using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class AsyncExtensions
    {
        public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(source.Any(predicate));
        }

    }
}
