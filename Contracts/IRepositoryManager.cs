namespace Contracts;

public interface IRepositoryManager
{
    IStoreRepository Store { get; }
    IBuyerRepository Buyer { get; }
    IOrderRepository Order { get; }
    IMealComponentRepository MealComponent { get; }
    Task SaveAsync();
}