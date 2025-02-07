using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiHomeTask1.Data;
using WebApiHomeTask1.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHomeTask1.Data.Contexts;
using WebApiHomeTask1.DTO.Requests.Author;
using WebApiHomeTask1.Services.Interfaces;

namespace WebApiHomeTask1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    /*private readonly LibraryDb _context;*/
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpPost]
    public async Task<ActionResult<Author>> AuthorAdd([FromBody] AuthorAddRequest request)
    {
        await _authorService.AddAuthorAsync(request);

        return Ok("Author has been added");
    }


    [HttpPost]
    public async Task<ActionResult<Author>> AuthorDelete([FromBody] AuthorDeleteRequest request)
    {
        if (_authorService.CheckAuthorExistance(request))
        {
            await _authorService.DeleteAuthorAsync(request);
            return Ok("Author has been deleted");

        }

        return StatusCode(666, "Author doesn't exist");

    }


    [HttpPost]
    public async Task<ActionResult<Author>> UpdateAuthor([FromBody] AuthorDeleteRequest request)
    {
        if (_authorService.CheckAuthorExistance(request))
        {


            await _authorService.UpdateAuthorAsync(request);
            return Ok("Author has been updated");
        }

        return StatusCode(666, "Author doesn't exist");
    }

    
    
    [HttpPost]
    public async Task<ActionResult<Author>> FindAuthor([FromBody] AuthorDeleteRequest request)
    {
        if (_authorService.CheckAuthorExistance(request))
        {


            var author =  await _authorService.FindAuthorAsync(request);
            return Ok(author);
        }

        return StatusCode(666, "Author doesn't exist");
    }
    
    
    
    
}
        
        
        
        
        
    
