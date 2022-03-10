using Entities.Models;

namespace Contracts;

public interface IMealComponentRepository
{
    IEnumerable<MealComponent> GetMealComponents(Guid storeId, bool trackChanges);
    MealComponent GetMealComponent(Guid storeId, Guid mealComponentId, bool trackChanges);
    void CreateMealComponentForStore(Guid storeId, MealComponent mealComponent);
    void DeleteMealComponent(MealComponent mealComponent);
}