namespace GameStoreProject.Data.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Games> Games { get; set; } = new List<Games>();
}