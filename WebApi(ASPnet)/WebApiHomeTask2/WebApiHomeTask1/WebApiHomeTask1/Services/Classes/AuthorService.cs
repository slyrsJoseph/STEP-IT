using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiHomeTask1.Data.Contexts;
using WebApiHomeTask1.Data.Models;
using WebApiHomeTask1.DTO.Requests.Author;
using WebApiHomeTask1.Services.Interfaces;

namespace WebApiHomeTask1.Services.Classes;

public class AuthorService(LibraryDb context, IMapper mapper) : IAuthorService
{
    
    private readonly LibraryDb _context = context;
    private readonly IMapper _mapper = mapper;
    
    
    
    public async Task AddAuthorAsync(AuthorAddRequest request)
    {
        var author = _mapper.Map<Author>(request);

      
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync (AuthorDeleteRequest request)
    {
        var match = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorName == request.FindByName);
        
        _context.Authors.Remove(match);
        await _context.SaveChangesAsync();
        
    }



    public bool CheckAuthorExistance(AuthorDeleteRequest request)
    {
        var match = _context.Authors.FirstOrDefaultAsync(x => x.AuthorName == request.FindByName);

        if (match == null)
        {

            return true;
        }
        else
        {

            return false;
        }

    }

    public async Task UpdateAuthorAsync(AuthorDeleteRequest request)
    {
            var match = _context.Authors.FirstOrDefault(x => x.AuthorName == request.FindByName);
            match = _mapper.Map<Author>(request);
            await _context.SaveChangesAsync();  
            
            
            
    }

    public async Task<Author> FindAuthorAsync(AuthorDeleteRequest request)
    {
        var match = _context.Authors.FirstOrDefault(x => x.AuthorName == request.FindByName);
        return match;

    }

}


