using WebApiHomeTask1.Data.Models;
using WebApiHomeTask1.DTO.Requests.Book;

namespace WebApiHomeTask1.DTO.Requests.Author;

public record AuthorAddRequest(string AuthorName, BookRequest[] AuthorBooks);