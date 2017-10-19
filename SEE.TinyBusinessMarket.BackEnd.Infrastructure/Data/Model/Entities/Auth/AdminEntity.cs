//-----------------------------------------------------------------------
// <copyright file="AdminEntity.cs" company="SEE Software">
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


    [Table("adm_admins", Schema = "public")]
    public class AdminEntity
    {
        [Key]
        [Column("adm_id")]
        public Guid Id { get; set; }

        [Column("adm_login")]
        public string Login { get; set; }

        [Column("adm_pass")]
        public string Password { get; set; }

        [Column("adm_first_name")]
        public string FirstName { get; set; }

        [Column("adm_last_name")]
        public string LastName { get; set; }

        [Column("adm_active")]
        public bool Active { get; set; }
    }
}
