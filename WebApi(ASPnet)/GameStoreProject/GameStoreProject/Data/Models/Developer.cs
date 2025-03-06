namespace GameStoreProject.Data.Models;

public class Developer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Website { get; set; }
    public string Country { get; set; }

    public ICollection<Game> Games { get; set; } = new List<Game>();
}