using Entities.Models;

namespace Contracts;

public interface IMealComponentRepository
{
    Task<IEnumerable<MealComponent>> GetMealComponentsAsync(Guid storeId, bool trackChanges);
    Task<MealComponent> GetMealComponentAsync(Guid storeId, Guid mealComponentId, bool trackChanges);
    void CreateMealComponentForStore(Guid storeId, MealComponent mealComponent);
    void DeleteMealComponent(MealComponent mealComponent);
}