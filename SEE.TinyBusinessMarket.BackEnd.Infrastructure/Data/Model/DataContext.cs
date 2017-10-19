//-----------------------------------------------------------------------
// <copyright file="DataContext.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LicenceEntity>().HasOne(x => x.Invoice).WithOne(x => x.Licence);
            modelBuilder.Entity<OrderEntity>().HasOne(x => x.ProForma).WithOne(x => x.Order);

            base.OnModelCreating(modelBuilder);
        }

        #region prod
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<LicenceEntity> Licences { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<TokenEntity> Tokens { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<ProFormaEntity> ProForma { get; set; }
        public DbSet<SettingEntity> Settings { get; set; }
        #endregion

        #region dict
        public DbSet<OrderStatusEntity> OrderStatus { get; set; }
        public DbSet<VatRateEntity> VatRates { get; set; }
        public DbSet<OrderTypeEntity> OrderTypes { get; set; }
        public DbSet<PaymentTypeEntity> PaymentTypes { get; set; }

        #endregion

        #region auth
        public DbSet<AdminEntity> Admins { get; set; }
        #endregion
    }
}
