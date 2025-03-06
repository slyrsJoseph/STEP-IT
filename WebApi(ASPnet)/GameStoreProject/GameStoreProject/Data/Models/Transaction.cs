namespace GameStoreProject.Data.Models;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public string PaymentMethod { get; set; }
    public string Status { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}