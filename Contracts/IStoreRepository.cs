using Entities.Models;

namespace Contracts;

public interface IStoreRepository
{
    IEnumerable<Store> GetAllStores(bool trackChanges);
    Store GetStore(Guid storeId, bool trackChanges);
    void CreateStore(Store store);
    IEnumerable<Store> GetStoresByIds(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteStore(Store store);
}