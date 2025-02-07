namespace WebApiHomeTask1.Data.Models;

public class Author
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AuthorName { get; set; }

    public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    
}