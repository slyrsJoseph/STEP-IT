using WebApiHomeTask1.DTO.Requests;
using WebApiHomeTask1.DTO.Requests.Book;

namespace WebApiHomeTask1.Services.Classes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiHomeTask1.Data.Contexts;
using WebApiHomeTask1.Data.Models;
using WebApiHomeTask1.DTO.Requests.Author;
using WebApiHomeTask1.Services.Interfaces;

public class BookService(LibraryDb context, IMapper mapper) : IBookService
{
    private readonly LibraryDb _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task AddBookAsync(BookRequest request)
    {
        var book = _mapper.Map<Book>(request);
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(BookRequest request)
    {
        var match = await _context.Books.FirstOrDefaultAsync(x => x.BookName == request.BookName);
        _context.Books.Remove(match);
        await _context.SaveChangesAsync();
    }

    public bool CheckBookExists(BookRequest request)
    {
        var match = _context.Books.FirstOrDefault(x => x.BookName == request.BookName);

        if (match != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task UpdateBookAsync(BookRequest request)
    {
        var match = await _context.Books.FirstOrDefaultAsync(x => x.BookName == request.BookName);
        match.BookName = request.BookName;
        await _context.SaveChangesAsync();
    }

    public async Task<Book> FindBookAsync(BookRequest request)
    {
        var match = await _context.Books.FirstOrDefaultAsync(x => x.BookName == request.BookName);
        return match;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}