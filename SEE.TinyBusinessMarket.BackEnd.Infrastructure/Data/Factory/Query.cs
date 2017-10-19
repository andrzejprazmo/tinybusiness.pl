//-----------------------------------------------------------------------
// <copyright file="Query.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Factory
{
    using SEE.Framework.Core.Abstract;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class Query : IQuery
    {
        private readonly DataContext _dataContext;
        public Query(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _dataContext.Set<TEntity>().First(predicate);
        }

        public TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _dataContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : class
        {
            return _dataContext.Set<TEntity>().AsQueryable();
        }

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _dataContext.Set<TEntity>().Single(predicate);
        }

        public TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _dataContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        #region async
        public async Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _dataContext.Set<TEntity>().FirstAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _dataContext.Set<TEntity>().SingleAsync(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _dataContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }


        #endregion
    }
}
