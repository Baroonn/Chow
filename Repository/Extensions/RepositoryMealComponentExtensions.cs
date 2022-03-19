using Entities.Models;

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
}