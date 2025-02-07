namespace WebApiHomeTask1.Data.Models;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string BookName { get; set; }

    public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}