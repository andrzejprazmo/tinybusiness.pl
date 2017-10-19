//-----------------------------------------------------------------------
// <copyright file="TokenExtensions.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;


    public static class TokenExtensions
    {
        public static IQueryable<TokenContract> SelectTokenContract(this IQueryable<TokenEntity> query) => query.Select(x => new TokenContract
        {
            Id = x.Id,
            Active = x.Active,
            Created = x.Created,
            Data = x.Data,
            Expires = x.Expires,
            CustomerId = x.CustomerId,
            CustomerFirstName = x.Customer.FirstName,
            CustomerLastName = x.Customer.LastName,
            CustomerEmail = x.Customer.Email,
        });
    }
}
