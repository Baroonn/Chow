using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IOrderRepository
{
    Task<PagedList<Order>> GetOrdersForBuyerAsync(Guid buyerId, OrderParameters orderParameters, bool trackChanges);
    Task<Order> GetSingleOrderForBuyerAsync(Guid buyerId, Guid orderId, bool trackChanges);
    void CreateOrderForBuyer(Guid buyerId, Guid storeId, Order order);
    Task<PagedList<Order>> GetOrdersForStoreAsync(Guid storeId, OrderParameters orderParameters, bool trackChanges);
    Task<Order> GetSingleOrderForStoreAsync(Guid storeId, Guid orderId, bool trackChanges);
}