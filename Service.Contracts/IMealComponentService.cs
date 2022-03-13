using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IMealComponentService
{
    IEnumerable<MealComponentReadDto> GetMealComponents(Guid storeId, bool trackChanges);
    MealComponentReadDto GetMealComponent(Guid storeId, Guid mealComponentId, bool trackChanges);
    MealComponentReadDto CreateMealComponentForStore(Guid storeId, MealComponentCreateDto mealComponentCreateDto, bool trackChanges);
    void DeleteMealComponentForStore(Guid storeId, Guid id, bool trackChanges);
    void UpdateMealComponentForStore(Guid storeId, Guid id, MealComponentUpdateDto mealComponentUpdateDto, bool trackStoreChanges, bool trackMealComponentChanges);
    (MealComponentUpdateDto mealComponentToPatch, MealComponent mealComponent) GetMealComponentForPatch(
        Guid storeId, Guid id, bool trackStoreChanges, bool trackMealComponentChanges
    );

    void SaveChangesForPatch(MealComponentUpdateDto mealComponentToPatch, MealComponent mealComponent);
}