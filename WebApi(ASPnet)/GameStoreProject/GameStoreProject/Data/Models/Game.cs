namespace GameStoreProject.Data.Models;

public class Game
{
    public int Id { get; set; }
    
    public string Description { get; set; }
    
    public decimal price { get; set; }

    public string title { get; set; }
    
    public DateTime ReleaseDate { get; set; }

    public int DeveloperId { get; set; }
    public Developer Developer { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

}