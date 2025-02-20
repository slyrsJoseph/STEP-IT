namespace WebApiHomeTask1.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using WebApiHomeTask1.Data.Models;
using WebApiHomeTask1.Data.Config;

public class LibraryDb : DbContext
{
    public LibraryDb(DbContextOptions<LibraryDb> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDb).Assembly);
        /*modelBuilder.ApplyConfiguration(new AuthorsConfig());
        modelBuilder.ApplyConfiguration(new BooksConfig());
        modelBuilder.ApplyConfiguration(new GenresConfig());
        modelBuilder.ApplyConfiguration(new LibraryConfig());*/
    }
}
