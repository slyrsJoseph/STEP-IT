namespace GameStoreProject.Data.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    // ❗ Добавляем связь с Transaction, чтобы EF Core корректно строил отношение 1:1
    public Transaction Transaction { get; set; }
}
