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


    [Table("pyt_payment_types", Schema = "public")]
    public class PaymentTypeEntity
    {
        [Key]
        [Column("pyt_id")]
        public int Id { get; set; }

        [Column("pyt_name")]
        public string Name { get; set; }

        [Column("pyt_code")]
        public string Code { get; set; }

        [Column("pyt_sort")]
        public int Sort { get; set; }
    }
}
