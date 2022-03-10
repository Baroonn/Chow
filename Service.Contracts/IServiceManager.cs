namespace Service.Contracts;

public interface IServiceManager
{
    IBuyerService BuyerService { get; }
    IMealComponentService MealComponentService { get; }
    IOrderService OrderService { get; }
    IStoreService StoreService { get; }
}