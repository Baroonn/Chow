
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IStoreService
{
    IEnumerable<StoreReadDto> GetAllStores(bool trackChanges);
    StoreReadDto GetStore(Guid storeId, bool trackChanges);
    StoreReadDto CreateStore(StoreCreateDto store);
    IEnumerable<StoreReadDto> GetStoresByIds(IEnumerable<Guid> ids, bool trackChanges);
    (IEnumerable<StoreReadDto> storeReadDtoCollection, string ids) CreateStoreCollection(IEnumerable<StoreCreateDto> storeCollection);
    void DeleteStore(Guid storeId, bool trackChanges);
    void UpdateStore(Guid storeId, StoreUpdateDto storeUpdateDto, bool trackChanges);
}