using Microsoft.AspNetCore.Mvc;
using NoScrip.Db;
using NoScrip.Models;
using NoScrip.Lib;
namespace server.Controllers;



[ApiController]
[ApiV1("[Controller]")]
public class Reload : ControllerBase
{
    private readonly ILogger<Reload> _logger;
    private readonly DataContext _dbContext;

    public Reload(ILogger<Reload> logger, DataContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }


    [HttpGet]
    public async Task<IActionResult> GetReload()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();
        // await _dbContext.AddRangeAsync(DefaultData.GetBookStatus());
        await _dbContext.AddRangeAsync(DefaultData.GetGenres());
        await _dbContext.AddRangeAsync(DefaultData.GetServer());
        await _dbContext.SaveChangesAsync();
        return Ok(new { message = "reload ok" });
    }

    [HttpPost("test")]
    public async Task<IActionResult> PostTest(string name)
    {
        var newBook = new Book() { Name = name };
        await _dbContext.Book.AddAsync(newBook);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    [HttpPost("testx")]
    public async Task<IActionResult> PostTestx(int id, BookStatus status, string description)
    {
        var newBook = new BookDetail()
        {
            BookDetailId = id,
            BookStatus = status,
            Description = description
        };
        await _dbContext.BookDetail.AddAsync(newBook);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("bGenres")]
    public async Task<IActionResult> PostBGenres(int bookDetailId, int genresId)
    {
        var newBookGenres = new BookGenres() { BookDetailId = bookDetailId, GenresId = genresId };
        await _dbContext.BookGenres.AddRangeAsync(newBookGenres);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
