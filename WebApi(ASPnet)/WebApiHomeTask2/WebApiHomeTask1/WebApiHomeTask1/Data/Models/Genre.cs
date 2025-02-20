namespace WebApiHomeTask1.Data.Models;

public class Genre
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string GenreName { get; set; }

    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
  
}