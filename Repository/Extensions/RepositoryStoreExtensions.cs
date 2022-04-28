using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions;

public static class RepositoryStoreExtensions
{
    public static IQueryable<Store> Paginate(this IQueryable<Store> stores, int pageNumber, int pageSize)
    {
        return stores
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);
    }

    public static IQueryable<Store> Sort(this IQueryable<Store> stores, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return stores.OrderBy(s => s.Name);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Store>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return stores.OrderBy(s => s.Name);
        }

        return stores.OrderBy(orderQuery);
    }

    // public static IQueryable<Store> SearchForMealComponent(this IQueryable<Store> stores, string searchTerm)
    // {
    //     if (string.IsNullOrWhiteSpace(searchTerm))
    //     {
    //         return stores;
    //     }
    //     return stores.Where()
    // }
}