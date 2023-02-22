using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoScrip.Db;
using NoScrip.Lib;
using NoScrip.Models;

namespace server.Controllers;

[ApiV1("book")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly IConfiguration _Configuration;
    private readonly DataContext _dbContext;
    public BookController(
        ILogger<BookController> logger,
        DataContext dbContext,
        IConfiguration Configuration)
    {
        _logger = logger;
        _dbContext = dbContext;
        _Configuration = Configuration;

    }
    [HttpPost("create")]
    [Consumes("application/json")]



    /*
      name,author,description : string
      bookStatus:int
      avatar:{
        serverId:int,
        url
      }

      
    */
    public async Task<IActionResult> PostCreate([FromBody] BookForm bookForm)
    {
        try
        {
            var newBook = new Book()
            {
                Name = bookForm.Name
            };
            await _dbContext.Book.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            var newBookDetail = new BookDetail()
            {
                Description = bookForm.Description,
                Author = bookForm.Author,
                BookStatus = bookForm.BookStatus,
                BookDetailId = newBook.BookId
            };
            await _dbContext.BookDetail.AddAsync(newBookDetail);
            await _dbContext.SaveChangesAsync();




            bookForm?.Avatars?.ForEach(async av =>
            {
                var newAvatar = new BookAvatar()
                {
                    Url = av.Url,
                    ServerId = av.ServerId,
                    BookDetailId = newBookDetail.BookDetailId
                };
                await _dbContext.BookAvatar.AddAsync(newAvatar);
            });

            return Ok(new
            {
                BookId = newBook.BookId,
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
