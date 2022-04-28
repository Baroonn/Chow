using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IOrderService
{
    Task<IEnumerable<OrderReadDto>> GetAllOrdersForBuyerAsync(Guid buyerId, bool trackChanges);
    Task<OrderReadDto> GetOrderForBuyerAsync(Guid buyerId, Guid orderId, bool trackChanges);
    Task<OrderReadDto> CreateOrderForBuyerAsync(Guid buyerId, OrderCreateDto orderCreateDto, bool trackChanges);
    Task<IEnumerable<OrderReadDto>> GetAllOrdersForStoreAsync(Guid storeId, bool trackChanges);
    Task<OrderReadDto> GetOrderForStoreAsync(Guid storeId, Guid orderId, bool trackChanges);
    
}