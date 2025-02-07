namespace WebApiHomeTask1.Data.Models;

public class AuthorBook
{
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; }
}