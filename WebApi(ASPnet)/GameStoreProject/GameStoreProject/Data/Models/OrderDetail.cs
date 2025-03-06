namespace GameStoreProject.Data.Models;

public class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

   public int GameId { get; set; }
    public Game Game { get; set; }

    // public ICollection<Game> Game { get; set; } = new List<Game>();
    public decimal PriceAtPurchase { get; set; }
}