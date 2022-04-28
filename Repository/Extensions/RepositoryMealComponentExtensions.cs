using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;
namespace Repository.Extensions;

public static class RepositoryMealComponentExtensions
{
    public static IQueryable<MealComponent> Paginate(this IQueryable<MealComponent> mealComponents, int pageNumber, int pageSize)
    {
        return mealComponents
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);
    }

    public static IQueryable<MealComponent> FilterByType(this IQueryable<MealComponent> mealComponents, string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            return mealComponents;
        }
        var filterType = type.Trim().ToLower();
        return mealComponents.Where(m => m.Type.ToLower() == filterType);
    }

    public static IQueryable<MealComponent> Search(this IQueryable<MealComponent> mealComponents, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return mealComponents;
        }

        var lowercaseSearchTerm = searchTerm.Trim().ToLower();
        return mealComponents.Where(m => m.Name.ToLower().Contains(lowercaseSearchTerm));
    }

    public static IQueryable<MealComponent> Sort(this IQueryable<MealComponent> mealComponents, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return mealComponents.OrderBy(s => s.Name);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<MealComponent>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return mealComponents.OrderBy(m => m.Name);
        }

        return mealComponents.OrderBy(orderQuery);
    }
}