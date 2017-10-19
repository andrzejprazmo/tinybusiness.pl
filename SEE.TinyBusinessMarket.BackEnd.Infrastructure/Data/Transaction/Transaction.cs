//-----------------------------------------------------------------------
// <copyright file="TransactionScope.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Transaction
{
    using Microsoft.EntityFrameworkCore.Storage;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System;
	using System.Collections.Generic;
	using System.Text;

    public interface ITransaction
    {
        TransactionScope BeginTransaction();
    }

    public class Transaction : ITransaction
    {
        private readonly DataContext _dataContext;
        public Transaction(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public TransactionScope BeginTransaction() => TransactionScope.Create(_dataContext);
    }

    public class TransactionScope : IDisposable
    {
        private readonly IDbContextTransaction _transaction;

        protected TransactionScope(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public static TransactionScope Create(DataContext dataContext)
        {
            return new TransactionScope(dataContext.Database.BeginTransaction());
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }
    }
}
