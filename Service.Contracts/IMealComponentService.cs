using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IMealComponentService
{
    Task<(IEnumerable<MealComponentReadDto> mealComponentReadDtos, PaginationMetaData paginationMetaData)> GetMealComponentsAsync(Guid storeId, MealComponentParameters mealComponentParameters ,bool trackChanges);
    Task<MealComponentReadDto> GetMealComponentAsync(Guid storeId, Guid mealComponentId, bool trackChanges);
    Task<MealComponentReadDto> CreateMealComponentForStoreAsync(Guid storeId, MealComponentCreateDto mealComponentCreateDto, bool trackChanges);
    Task DeleteMealComponentForStoreAsync(Guid storeId, Guid id, bool trackChanges);
    Task UpdateMealComponentForStoreAsync(Guid storeId, Guid id, MealComponentUpdateDto mealComponentUpdateDto, bool trackStoreChanges, bool trackMealComponentChanges);
    Task<(MealComponentUpdateDto mealComponentToPatch, MealComponent mealComponent)> GetMealComponentForPatchAsync(
        Guid storeId, Guid id, bool trackStoreChanges, bool trackMealComponentChanges
    );

    Task SaveChangesForPatchAsync(MealComponentUpdateDto mealComponentToPatch, MealComponent mealComponent);
}