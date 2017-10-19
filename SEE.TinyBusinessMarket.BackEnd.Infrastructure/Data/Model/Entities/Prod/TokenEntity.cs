//-----------------------------------------------------------------------
// <copyright file="TokenEntity.cs" company="SEE Software">
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


    [Table("tkn_tokens", Schema = "public")]
    public class TokenEntity
    {
        #region fields
        [Key]
        [Column("tkn_id")]
        public Guid Id { get; set; }

        [Column("cst_id")]
        public Guid CustomerId { get; set; }

        [Column("tkn_data")]
        public string Data { get; set; }

        [Column("tkn_active")]
        public bool Active { get; set; }

        [Column("tkn_created")]
        public DateTime Created { get; set; }

        [Column("tkn_expires")]
        public DateTime Expires { get; set; }
        #endregion

        #region nav 1:n
        [ForeignKey(nameof(CustomerId))]
        public virtual CustomerEntity Customer { get; set; }
        #endregion
    }
}
