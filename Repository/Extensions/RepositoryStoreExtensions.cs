using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryStoreExtensions
{
    public static IQueryable<Store> Paginate(this IQueryable<Store> stores, int pageNumber, int pageSize)
    {
        return stores
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);
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