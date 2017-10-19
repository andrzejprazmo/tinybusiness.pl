//-----------------------------------------------------------------------
// <copyright file="SettingEntity.cs" company="SEE Software">
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


    [Table("set_settings", Schema = "public")]
    public class SettingEntity
    {
        [Key]
        [Column("set_id")]
        public int Id { get; set; }

        [Column("set_invoice_last_number")]
        public int InvoiceLastNumber { get; set; }
    }
}
