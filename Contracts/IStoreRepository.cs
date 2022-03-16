using Entities.Models;

namespace Contracts;

public interface IStoreRepository
{
    Task<IEnumerable<Store>> GetAllStoresAsync(bool trackChanges);
    Task<Store> GetStoreAsync(Guid storeId, bool trackChanges);
    void CreateStore(Store store);
    Task<IEnumerable<Store>> GetStoresByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteStore(Store store);
}