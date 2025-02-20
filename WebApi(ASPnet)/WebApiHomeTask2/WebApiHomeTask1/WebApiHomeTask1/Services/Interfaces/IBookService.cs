using WebApiHomeTask1.DTO.Requests.Book;

namespace WebApiHomeTask1.Services.Interfaces;
using WebApiHomeTask1.Data.Models;

public interface IBookService
{
    public  Task AddBookAsync(BookRequest request);
    public  Task DeleteBookAsync(BookRequest request);
    public bool CheckBookExists(BookRequest request);
    public  Task UpdateBookAsync(BookRequest request);
    public  Task<Book> FindBookAsync(BookRequest request);

}