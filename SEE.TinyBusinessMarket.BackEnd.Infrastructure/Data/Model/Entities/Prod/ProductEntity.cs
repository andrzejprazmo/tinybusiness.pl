//-----------------------------------------------------------------------
// <copyright file="ProductEntity.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("prd_products", Schema = "public")]
    public class ProductEntity
    {
        #region fields
        [Key]
        [Column("prd_id")]
        public Guid Id { get; set; }

        [Column("prd_full_name")]
        public string FullName { get; set; }

        [Column("prd_name")]
        public string Name { get; set; }

        [Column("prd_unit_price")]
        public decimal UnitPrice { get; set; }

        [Column("vat_id")]
        public int VatRateId { get; set; }

        [Column("prd_active")]
        public bool Active { get; set; }

        [Column("prd_version")]
        public string Version { get; set; }

        #endregion

        #region nav 1:n
        [ForeignKey(nameof(VatRateId))]
        public virtual VatRateEntity VatRate { get; set; }
        #endregion
    }
}
