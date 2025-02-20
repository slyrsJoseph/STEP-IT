using WebApiHomeTask1.Data.Models;
using WebApiHomeTask1.DTO.Requests.Author;
using WebApiHomeTask1.DTO.Requests;

namespace WebApiHomeTask1.Services.Interfaces;

public interface IAuthorService
{
    public  Task AddAuthorAsync(AuthorAddRequest request);
    
    public Task DeleteAuthorAsync(AuthorDeleteRequest request);
    
    
    public Task UpdateAuthorAsync(AuthorDeleteRequest request);
    
    
    public bool CheckAuthorExistance(AuthorDeleteRequest request);
    
    public Task<Author> FindAuthorAsync(AuthorDeleteRequest request);
    
}