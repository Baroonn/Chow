
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IStoreService
{
    Task<IEnumerable<StoreReadDto>> GetAllStoresAsync(bool trackChanges);
    Task<StoreReadDto> GetStoreAsync(Guid storeId, bool trackChanges);
    Task<StoreReadDto> CreateStoreAsync(StoreCreateDto store);
    Task<IEnumerable<StoreReadDto>> GetStoresByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<StoreReadDto> storeReadDtoCollection, string ids)> CreateStoreCollectionAsync(IEnumerable<StoreCreateDto> storeCollection);
    Task DeleteStoreAsync(Guid storeId, bool trackChanges);
    Task UpdateStoreAsync(Guid storeId, StoreUpdateDto storeUpdateDto, bool trackChanges);
    Task<(StoreUpdateDto storeToPatch, Store store)> GetStoreForPatchAsync(Guid storeId, bool trackChanges);
    Task SaveChangesForPatchAsync(StoreUpdateDto storeToPatch, Store store);
}