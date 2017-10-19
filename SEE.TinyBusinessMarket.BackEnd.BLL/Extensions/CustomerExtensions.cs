//-----------------------------------------------------------------------
// <copyright file="CustomerExtensions.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Extensions
{
    using SEE.Framework.Core.Collection;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Common.Request;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public static class CustomerExtensions
    {
        public static IQueryable<CustomerEntity> FindBy(this IQueryable<CustomerEntity> query, CustomerRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Phrase))
            {
                query = query.Where(x => x.Name.Contains(request.Phrase) || x.Nip.Contains(request.Phrase) || x.Orders.Any(y => y.ProForma.FullNumber.Contains(request.Phrase)));
            }
            return query;
        }

        public static IOrderedQueryable<CustomerEntity> SortBy(this IQueryable<CustomerEntity> query, CustomerRequest request)
        {
            switch (request.SortColumn)
            {
                default:
                case CustomerRequest.SortByCreated:
                    switch (request.SortDirection)
                    {
                        case SortDirection.Ascending:
                            return query.OrderBy(x => x.RegisterDate);
                        default:
                        case SortDirection.Descending:
                            return query.OrderByDescending(x => x.RegisterDate);
                    }
                case CustomerRequest.SortByName:
                    switch (request.SortDirection)
                    {
                        default:
                        case SortDirection.Ascending:
                            return query.OrderBy(x => x.Name);
                        case SortDirection.Descending:
                            return query.OrderByDescending(x => x.Name);
                    }
            }

        }



        public static IQueryable<CustomerContract> SelectCustomerContract(this IQueryable<CustomerEntity> query) => query.Select(x => new CustomerContract
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Name = x.Name,
            Nip = x.Nip,
            City = x.City,
            Email = x.Email,
            Phone = x.Phone,
            StreetNumber = x.StreetNumber,
            RegisterDate = x.RegisterDate,
            ZipCode = x.ZipCode,
        });
    }
}
