using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IMealComponentRepository
{
    Task<PagedList<MealComponent>> GetMealComponentsAsync(Guid storeId, MealComponentParameters mealComponentParameters, bool trackChanges);
    Task<MealComponent> GetMealComponentAsync(Guid storeId, Guid mealComponentId, bool trackChanges);
    void CreateMealComponentForStore(Guid storeId, MealComponent mealComponent);
    void DeleteMealComponent(MealComponent mealComponent);
}