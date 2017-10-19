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


    [Table("otp_order_types", Schema = "public")]
    public class OrderTypeEntity
    {
        [Key]
        [Column("otp_id")]
        public int Id { get; set; }

        [Column("otp_name")]
        public string Name { get; set; }

        [Column("otp_code")]
        public string Code { get; set; }

        [Column("otp_sort")]
        public int Sort { get; set; }
    }
}
