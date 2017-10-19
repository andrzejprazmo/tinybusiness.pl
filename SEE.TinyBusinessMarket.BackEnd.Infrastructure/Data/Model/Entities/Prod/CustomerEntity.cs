//-----------------------------------------------------------------------
// <copyright file="Customer.cs" company="SEE Software">
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


    [Table("cst_customers", Schema = "public")]
    public class CustomerEntity
    {
        #region fields
        [Key]
        [Column("cst_id")]
        public Guid Id { get; set; }

        [Column("cst_name")]
        public string Name { get; set; }

        [Column("cst_nip")]
        public string Nip { get; set; }

        [Column("cst_street_number")]
        public string StreetNumber { get; set; }

        [Column("cst_zip_code")]
        public string ZipCode { get; set; }

        [Column("cst_city")]
        public string City { get; set; }

        [Column("cst_email")]
        public string Email { get; set; }

        [Column("cst_phone")]
        public string Phone { get; set; }

        [Column("cst_first_name")]
        public string FirstName { get; set; }

        [Column("cst_last_name")]
        public string LastName { get; set; }

        [Column("cst_register_date")]
        public DateTime RegisterDate { get; set; }
        #endregion

        #region nav n:1
        public ICollection<OrderEntity> Orders { get; set; }
        #endregion
    }
}
