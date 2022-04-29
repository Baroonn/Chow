using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryOrderExtension
    {
        public static IQueryable<Order> Paginate(this IQueryable<Order> orders, int pageNumber, int pageSize)
        {
            return orders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<Order> Sort(this IQueryable<Order> orders, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return orders.OrderBy(o => o.Description);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Order>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return orders.OrderBy(o => o.Description);
            }

            return orders.OrderBy(orderQuery);
        }
    }
}
