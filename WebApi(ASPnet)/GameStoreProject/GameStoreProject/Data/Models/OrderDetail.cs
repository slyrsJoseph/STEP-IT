namespace GameStoreProject.Data.Models;

public class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int GameId { get; set; }
    public Games Games { get; set; }

    public decimal PriceAtPurchase { get; set; }
}