namespace GameStoreProject.Data.Repository.OrderRep;
using GameStoreProject.Data.Context;
using GameStoreProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using GameStoreProject.Data.Repository.MainRep;

public class OrderRep : Repository<Order>, IOrderRep
{
    public OrderRep(GameStoreContext context) : base(context)
    {
    }

    public async Task AddOrderToClient(User client, Order order, Game game, decimal price)
    {
        
        order.User = client;
        order.UserId = client.Id;

       
        var orderDetail = new OrderDetail
        {
            OrderId = order.Id,
            GameId = game.Id,
            Order = order,
            Game = game,
            PriceAtPurchase = price
        };

        
        await _dbSet.AddAsync(order);
        await _context.Set<OrderDetail>().AddAsync(orderDetail);

        await _context.SaveChangesAsync();
    }

    
    public async Task<Order> GetOrderInclude(int orderId)
    {
        return await _dbSet
            .Include(o => o.User) 
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

  
    public async Task<List<Order>> GetOrdersByUserId(int userId)
    {
        return await _dbSet
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails) 
            .ToListAsync();
    }

   
    public async Task<Order> GetOrderWithDetails(int orderId)
    {
        return await _dbSet
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Game)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
    
    
}