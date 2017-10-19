//-----------------------------------------------------------------------
// <copyright file="InvoiceEntity.cs" company="SEE Software">
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


    [Table("inv_invoices", Schema = "public")]
    public class InvoiceEntity
    {
        #region fields
        [Key]
        [Column("lcn_id")]
        public Guid LicenceId { get; set; }

        [Column("inv_number")]
        public string Number { get; set; }

        [Column("inv_exposure_date")]
        public DateTime ExposureDate { get; set; }

        [Column("inv_sell_date")]
        public DateTime SellDate { get; set; }

        [Column("inv_payment_days")]
        public int PaymentDays { get; set; }

        [Column("inv_payment_date")]
        public DateTime? PaymentDate { get; set; }

        [Column("pyt_id")]
        public int PaymentTypeId { get; set; }
        #endregion

        #region nav 1:n
        [ForeignKey(nameof(LicenceId))]
        public virtual LicenceEntity Licence { get; set; }

        [ForeignKey(nameof(PaymentTypeId))]
        public virtual PaymentTypeEntity PaymentType { get; set; }
        #endregion
    }
}
