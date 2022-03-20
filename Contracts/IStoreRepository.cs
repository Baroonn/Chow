using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IStoreRepository
{
    Task<PagedList<Store>> GetAllStoresAsync(StoreParameters storeParameters, bool trackChanges);
    Task<Store> GetStoreAsync(Guid storeId, bool trackChanges);
    void CreateStore(Store store);
    Task<IEnumerable<Store>> GetStoresByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteStore(Store store);
}