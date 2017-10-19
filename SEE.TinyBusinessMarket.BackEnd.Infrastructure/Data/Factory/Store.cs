//-----------------------------------------------------------------------
// <copyright file="Store.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Factory
{
    using SEE.Framework.Core.Abstract;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading.Tasks;

    public class Store : IStore
    {
        private readonly DataContext _dataContext;
        public Store(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dataContext.Set<TEntity>().Add(entity);
        }

        public void Create<TEntity>(TEntity entity) where TEntity : class
        {
            Add(entity);
            Save();
        }

        public async Task CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            Add(entity);
            await SaveAsync();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _dataContext.Set<TEntity>().Remove(entity);
            Save();
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dataContext.Set<TEntity>().Remove(entity);
            await SaveAsync();
        }

        public void Modify<TEntity>(TEntity entity) where TEntity : class
        {
            _dataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            Modify(entity);
            Save();
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            Modify(entity);
            await SaveAsync();
        }
    }
}
