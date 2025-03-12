using GameStoreProject.Data.Repository.MainRep;

namespace GameStoreProject.Data.Repository.OrderRep;
using GameStoreProject.Data.Models;

public interface IOrderRep : IRepository<Order>
{
    Task AddOrderToClient(User client, Order order, Game game, decimal price);
    Task<Order> GetOrderInclude(int orderId);
    
    Task<List<Order>> GetOrdersByUserId(int userId);
    
    Task<Order> GetOrderWithDetails(int orderId);
    
}