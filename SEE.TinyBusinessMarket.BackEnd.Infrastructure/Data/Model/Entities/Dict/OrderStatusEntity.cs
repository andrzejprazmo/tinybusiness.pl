//-----------------------------------------------------------------------
// <copyright file="TransactionStatusEntity.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;


    [Table("ost_order_status", Schema = "public")]
    public class OrderStatusEntity
    {
        [Key]
        [Column("ost_id")]
        public int Id { get; set; }

        [Column("ost_name")]
        public string Name { get; set; }

        [Column("ost_code")]
        public string Code { get; set; }

        [Column("ost_sort")]
        public int Sort { get; set; }
    }
}
