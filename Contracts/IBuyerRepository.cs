using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IBuyerRepository
{
    Task<PagedList<Buyer>> GetAllBuyersAsync(BuyerParameters buyerParameters, bool trackChanges);
    Task<Buyer> GetBuyerAsync(Guid buyerId, bool trackChanges);
    void CreateBuyer(Buyer buyer);
    void DeleteBuyer(Buyer buyer);
}