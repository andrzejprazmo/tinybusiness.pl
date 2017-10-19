//-----------------------------------------------------------------------
// <copyright file="IQuery.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.Framework.Core.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;


    public interface IQuery
    {
        IQueryable<TEntity> Get<TEntity>() where TEntity : class;
        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    }
}
