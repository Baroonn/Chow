using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IOrderService
{
    Task<(IEnumerable<OrderReadDto> buyerOrders, PaginationMetaData metaData)> GetAllOrdersForBuyerAsync(Guid buyerId, OrderParameters orderParameters, bool trackChanges);
    Task<OrderReadDto> GetOrderForBuyerAsync(Guid buyerId, Guid orderId, bool trackChanges);
    Task<OrderReadDto> CreateOrderForBuyerAsync(Guid buyerId, OrderCreateDto orderCreateDto, bool trackChanges);
    Task<(IEnumerable<OrderReadDto> storeOrders, PaginationMetaData metaData)> GetAllOrdersForStoreAsync(Guid storeId, OrderParameters orderParameters, bool trackChanges);
    Task<OrderReadDto> GetOrderForStoreAsync(Guid storeId, Guid orderId, bool trackChanges);
    
}