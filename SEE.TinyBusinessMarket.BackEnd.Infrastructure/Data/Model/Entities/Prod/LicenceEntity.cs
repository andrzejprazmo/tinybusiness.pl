//-----------------------------------------------------------------------
// <copyright file="LicenceEntity.cs" company="SEE Software">
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


    [Table("lcn_licences", Schema = "public")]
    public class LicenceEntity
    {
        #region fields
        [Key]
        [Column("lcn_id")]
        public Guid Id { get; set; }

        [Column("cst_id")]
        public Guid CustomerId { get; set; }

        [Column("prd_id")]
        public Guid ProductId { get; set; }

        [Column("lcn_valid_from")]
        public DateTime ValidFrom { get; set; }

        [Column("lcn_valid_to")]
        public DateTime ValidTo { get; set; }

        [Column("lcn_qantity")]
        public int Quantity { get; set; }
        #endregion

        #region nav 1:n
        [ForeignKey(nameof(CustomerId))]
        public virtual CustomerEntity Customer { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductEntity Product { get; set; }
        #endregion

        #region nav 1:1
        public virtual InvoiceEntity Invoice { get; set; }
        #endregion
    }
}
