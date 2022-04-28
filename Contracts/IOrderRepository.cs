using Entities.Models;

namespace Contracts;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersForBuyerAsync(Guid buyerId, bool trackChanges);
    Task<Order> GetSingleOrderForBuyerAsync(Guid buyerId, Guid orderId, bool trackChanges);
    void CreateOrderForBuyer(Guid buyerId, Guid storeId, Order order);
    Task<IEnumerable<Order>> GetOrdersForStoreAsync(Guid storeId, bool trackChanges);
    Task<Order> GetSingleOrderForStoreAsync(Guid storeId, Guid orderId, bool trackChanges);
}