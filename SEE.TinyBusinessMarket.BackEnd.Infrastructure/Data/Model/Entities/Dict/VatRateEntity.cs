//-----------------------------------------------------------------------
// <copyright file="VatRateEntity.cs" company="SEE Software">
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


    [Table("vat_vat_rates", Schema = "public")]
    public class VatRateEntity
    {
        [Key]
        [Column("vat_id")]
        public int Id { get; set; }

        [Column("vat_value")]
        public decimal Value { get; set; }

        [Column("vat_name")]
        public string Name { get; set; }

        [Column("vat_sort")]
        public int Sort { get; set; }
    }
}
