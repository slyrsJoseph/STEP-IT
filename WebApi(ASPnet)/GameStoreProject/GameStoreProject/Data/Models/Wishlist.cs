namespace GameStoreProject.Data.Models;

public class Wishlist
{
  
    public int Id { get; set; }
    
   
    public int UserId { get; set; }
    public User User { get; set; }
    
  
    public int GameId { get; set; }
   public Game Game { get; set; }
   
   // public ICollection<Game> Games { get; set; } = new List<Game>();
}