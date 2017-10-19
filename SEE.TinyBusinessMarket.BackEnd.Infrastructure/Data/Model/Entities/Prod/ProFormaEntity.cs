//-----------------------------------------------------------------------
// <copyright file="ProForma.cs" company="SEE Software">
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


    [Table("prf_pro_forma", Schema = "public")]
    public class ProFormaEntity
    {
        #region fields
        [Key]
        [Column("ord_id")]
        public Guid OrderId { get; set; }

        [Column("prf_number")]
        public int Number { get; set; }

        [Column("prf_full_number")]
        public string FullNumber { get; set; }

        [Column("prf_exposure_date")]
        public DateTime ExposureDate { get; set; }

        #endregion

        #region nav 1:n
        [ForeignKey(nameof(OrderId))]
        public virtual OrderEntity Order { get; set; }

        #endregion
    }
}
