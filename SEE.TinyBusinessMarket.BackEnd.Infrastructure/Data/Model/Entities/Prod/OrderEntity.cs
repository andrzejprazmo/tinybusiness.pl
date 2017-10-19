//-----------------------------------------------------------------------
// <copyright file="TransactionEntity.cs" company="SEE Software">
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


    [Table("ord_orders", Schema = "public")]
    public class OrderEntity
    {
        #region fields
        [Key]
        [Column("ord_id")]
        public Guid Id { get; set; }

        [Column("cst_id")]
        public Guid CustomerId { get; set; }

        [Column("prd_id")]
        public Guid ProductId { get; set; }

        [Column("ost_id")]
        public int OrderStatusId { get; set; }

        [Column("ord_start")]
        public DateTime Start { get; set; }

        [Column("ord_finish")]
        public DateTime? Finish { get; set; }

        [Column("ord_order_id")]
        public string PayuOrderId { get; set; }

        [Column("ord_ext_order_id")]
        public string PayuExtOrderId { get; set; }

        [Column("ord_token")]
        public string Token { get; set; }

        [Column("otp_id")]
        public int OrderTypeId { get; set; }

        [Column("ord_quanity")]
        public int Quantity { get; set; }

        #endregion

        #region nav 1:n
        [ForeignKey(nameof(CustomerId))]
        public virtual CustomerEntity Customer { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductEntity Product { get; set; }

        [ForeignKey(nameof(OrderStatusId))]
        public virtual OrderStatusEntity OrderStatus { get; set; }

        [ForeignKey(nameof(OrderTypeId))]
        public virtual OrderTypeEntity OrderType { get; set; }

        #endregion

        #region nav 1:1
        public virtual ProFormaEntity ProForma { get; set; }

        #endregion
    }
}
