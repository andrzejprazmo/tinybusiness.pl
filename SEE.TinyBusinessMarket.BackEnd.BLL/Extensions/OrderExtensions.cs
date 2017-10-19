//-----------------------------------------------------------------------
// <copyright file="OrderExtensions.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.BLL.Extensions
{
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.TinyBusinessMarket.BackEnd.Infrastructure.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public static class OrderExtensions
    {
        public static IQueryable<OrderContract> SelectOrderContract(this IQueryable<OrderEntity> query) => query.Select(x => new OrderContract
        {
            Id = x.Id,
            CustomerId = x.CustomerId,
            Finish = x.Finish,
            OrderStatusId = x.OrderStatusId,
            OrderTypeId = x.OrderTypeId,
            PayuExtOrderId = x.PayuExtOrderId,
            PayuOrderId = x.PayuOrderId,
            ProductId = x.ProductId,
            Start = x.Start,
            Token = x.Token,
            Quantity = x.Quantity,
            OrderTypeName = x.OrderType.Name,
            ProductName = x.Product.Name,
            ProductFullName = x.Product.FullName,
            OrderStatusName = x.OrderStatus.Name,
            OrderTypeCode = x.OrderType.Code,
            OrderStatusCode = x.OrderStatus.Code,
        });
    }
}
