using Microsoft.AspNetCore.Authorization;
using WebApiHomeTask1.Data.Models;
using WebApiHomeTask1.DTO.Requests;
using WebApiHomeTask1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApiHomeTask1.DTO.Requests.Book;


namespace WebApiHomeTask1.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;


    
    [HttpPost("add-book")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> AddBook([FromBody] BookRequest request)
    {
        await _bookService.AddBookAsync(request);
        
        return Ok("Book added");
    }
    
    
    

    [HttpPost("delete-book")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteBook([FromBody] BookRequest request)
    {
        if (_bookService.CheckBookExists(request))
        {
            await _bookService.DeleteBookAsync(request);
            return Ok("Book deleted");
        }
        return StatusCode(666, "No book has been found");
    }
    
    
    
    [HttpPost("update-book")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> UpdateBook([FromBody] BookRequest request)
    {
        if (_bookService.CheckBookExists(request))
        {
            await _bookService.UpdateBookAsync(request);
            return Ok("Book updated");
        }
        return StatusCode(666, "No book has been found");
    }
    
    
    

    [HttpGet("find-book")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<IActionResult> FindBook ([FromQuery] BookRequest request)
    {
        if (_bookService.CheckBookExists(request))
        {
            var book =  await _bookService.FindBookAsync(request);
            return Ok(book);
        }
        else
        {
            return StatusCode(666, "No book has been found");
        }
    }
    
    
}