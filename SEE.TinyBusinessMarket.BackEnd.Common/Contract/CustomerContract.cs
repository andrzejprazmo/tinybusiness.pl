//-----------------------------------------------------------------------
// <copyright file="CustomerContract.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Contract
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class CustomerContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
