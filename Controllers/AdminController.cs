using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoScrip.Db;
using NoScrip.Lib;
using NoScrip.Models;
// using NoScrip.Utils;


namespace server.Controllers;

[ApiV1("admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly IConfiguration _Configuration;
    private readonly DataContext _dbContext;
    public AdminController(
        ILogger<AdminController> logger,
        DataContext dbContext,
        IConfiguration Configuration)
    {
        _logger = logger;
        _dbContext = dbContext;
        _Configuration = Configuration;

    }



    // todo book



    [HttpPost("book")]
    [Consumes("application/json")]
    public async Task<IActionResult> PostBook([FromBody] BookForm bookForm)
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

            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                BookId = newBook.BookId,
                // BookDetailId = newBookDetail.BookDetailId
            });

        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
    [HttpGet("book/{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        try
        {


            var bookDetail = await _dbContext.BookDetail
                            .FirstOrDefaultAsync(bd => bd.BookDetailId == id);


            if (bookDetail == null) return BadRequest();


            await _dbContext.Entry(bookDetail).Reference(bd => bd.Book).LoadAsync();

            await _dbContext.Entry(bookDetail).Collection(bd => bd.BookAvatars).LoadAsync();
            await _dbContext.Entry(bookDetail).Collection(bd => bd.BookGenres).LoadAsync();
            await _dbContext.Entry(bookDetail).Collection(bd => bd.BookChapter).LoadAsync();



            var bookAvatar = bookDetail?.BookAvatars?
            .Select(a => new
            {
                serverId = a.ServerId,
                url = a.Url
            });

            var bookGenres = bookDetail?.BookGenres?
            .Select(g => new { genresId = g.GenresId });

            var BookChapter = bookDetail?.BookChapter?
            .Select(bc => new { bookChapterId = bc.BookChapterId, chapter = bc.Count });

            return Ok(
            new
            {

                name = bookDetail?.Book?.Name,
                author = bookDetail?.Author,
                description = bookDetail?.Description,
                bookStatus = bookDetail?.BookStatus,
                created_at = bookDetail?.Book?.Create_at.ToString("MM/dd/yyyy hh:mm tt"),
                update_at = bookDetail?.Book?.Update_at.ToString("MM/dd/yyyy hh:mm tt"),
                bookAvatar,
                bookGenres,
                BookChapter
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    [HttpGet("book")]
    public async Task<IActionResult> GetBookPage(int page, int limit)
    {
        try
        {


            var books = await _dbContext.Book
            .Skip((page - 1) * limit)
            .Take(limit)
            .Select(b => new
            {
                bookId = b.BookId,
                name = b.Name,
                created_at = b.Create_at.ToString("MM/dd/yyyy hh:mm tt"),
                update_at = b.Update_at.ToString("MM/dd/yyyy hh:mm tt"),
            })
            .ToListAsync();
            return Ok(
                books
            );

        }
        catch
        {

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("book/{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {

            var currentBook = await _dbContext.Book.FirstOrDefaultAsync(b => b.BookId == id);
            if (currentBook == null) return BadRequest();
            _dbContext.Remove(currentBook);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }
        catch
        {


            return StatusCode(StatusCodes.Status500InternalServerError);

        }
    }



    // todo BookAvatar



    //  [HttpPost("")]
    //   public async Task<IActionResult> Post(){ return Ok();}

    // [HttpGet("book")]
    // public async Task<IActionResult> GetBook(int page, int limit)
    // {
    //     var count = await _dbContext.Book.CountAsync();
    //     var result = await _dbContext.Book
    //     .Skip((page - 1) * limit)
    //     .Take(limit)
    //     .Select(b => new
    //     {
    //         bookId = b.Id,
    //         name = b.Name,
    //         avatars = _dbContext.BookAvatar
    //                    .Where(a => a.BookId == b.Id)
    //                    .Select(a => new
    //                    {
    //                        url = a.Url,
    //                        domain = _dbContext.Server
    //                        .Where(s => s.Id == a.ServerId)
    //                        .Select(s => s.Domain)
    //                        .FirstOrDefault()
    //                    })
    //                   .ToList()
    //     })
    //     .ToListAsync();
    //     return Ok(new
    //     { 
    //         des = new
    //         {
    //             page,
    //             limit,
    //             total = Functions.TotalPage(count, page, limit)
    //         },
    //         result
    //     });
    // }

    // [HttpPost("book")]
    // [Consumes("application/json")]

    // public async Task<IActionResult> PostBook([FromBody] bookForm book)
    // {


    //     var newBook = new Book()
    //     {
    //         Name = book.Name,
    //         Author = book.Author,
    //         About = book.About
    //     };
    //     try
    //     {
    //         await _dbContext.Book.AddAsync(newBook);
    //         await _dbContext.SaveChangesAsync();


    //         book.Avatars.ForEach(a =>
    //         {
    //             _dbContext.BookAvatar.Add(new BookAvatar()
    //             {
    //                 BookId = newBook.Id,
    //                 Url = a.Url,
    //                 ServerId = a.ServerId
    //             });
    //         });
    //         await _dbContext.SaveChangesAsync();
    //         return Ok();

    //     }
    //     catch
    //     {
    //         var deleteBook = await _dbContext.Book
    //         .FirstOrDefaultAsync(b => b.Id == newBook.Id);
    //         if (deleteBook != null)
    //         {
    //             _dbContext.Remove(deleteBook);
    //             await _dbContext.SaveChangesAsync();
    //         }
    //         return BadRequest();
    //     }
    // }



    //todo server





    [HttpPost("server")]
    [Consumes("application/json")]
    public async Task<IActionResult> PostSever([FromBody] ServerForm serverForm)
    {
        try
        {
            var newSever = new Server()
            {
                Name = serverForm.Name,
                Domain = serverForm.Domain
            };
            await _dbContext.Server.AddAsync(newSever);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("server/{id}")]
    [Consumes("application/json")]
    public async Task<IActionResult> PutServer(int id, [FromBody] ServerForm serverForm)
    {
        try
        {

            var server = await _dbContext.Server
            .FirstOrDefaultAsync(s => s.ServerId == id);

            if (server != null)
            {
                if (serverForm.Domain == server?.Domain && serverForm.Name == server?.Name)
                    return BadRequest();
                server.Name = serverForm.Name;
                server.Domain = serverForm.Domain;

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();




        }
        catch { return StatusCode(StatusCodes.Status500InternalServerError); }
    }

    [HttpGet("server")]
    public async Task<IActionResult> GetServer()
    {
        try
        {
            var servers = await _dbContext.Server.ToListAsync();
            return Ok(new
            {
                total = servers.Count(),
                servers

            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


    [HttpDelete("server/{id}")]
    public async Task<IActionResult> DeleteSever(int id)
    {
        try
        {
            var server = await _dbContext.Server
            .FirstOrDefaultAsync(s => s.ServerId == id);
            if (server == null) return BadRequest();
            _dbContext.Remove(server);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }










    // todo genres
    // [HttpGet("genres")]
    // public async Task<IActionResult> GetGenres(string word = "22022000")
    // {

    //     var total = await _dbContext.Genres.CountAsync();
    //     var result = await _dbContext.Genres
    //                  .Select(g => new
    //                  {
    //                      genresId = g.Id,
    //                      tag = g.Tag
    //                  })
    //                 .ToListAsync();
    //     if (word != "22022000")
    //         result = result.Where(g => g.tag.Contains(word)).ToList();
    //     var count = result.Count();
    //     return Ok(new
    //     {
    //         des = new
    //         {
    //             total,
    //             count

    //         },
    //         result

    //     });

    // }

    // [HttpPost("genres")]
    // [Consumes("application/json")]

    // public async Task<IActionResult> PostGenres([FromBody] Genres genres)
    // {

    //     try
    //     {
    //         var newGenres = new Genres() { Tag = genres.Tag };
    //         await _dbContext.Genres.AddAsync(newGenres);
    //         await _dbContext.SaveChangesAsync();
    //         return Ok(new
    //         {
    //             newGenres
    //         });
    //     }
    //     catch
    //     {

    //         return BadRequest();
    //     }

    // }

    // [HttpPut("genres/{id}")]
    // [Consumes("application/json")]

    // public async Task<IActionResult> PutGenres(int id, [FromBody] Genres fixGenres)
    // {
    //     try
    //     {
    //         var genres = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
    //         if (genres == null) return BadRequest();
    //         genres.Tag = fixGenres.Tag;
    //         await _dbContext.SaveChangesAsync();
    //         return Ok(
    //             genres
    //         );
    //     }
    //     catch
    //     {

    //         return BadRequest();
    //     }

    // }
    // [HttpDelete("genres/{id}")]
    // [Consumes("application/json")]
    // public async Task<IActionResult> DeleteGenres(int id)
    // {
    //     var genres = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
    //     if (genres == null) return BadRequest();
    //     _dbContext.Remove(genres);
    //     await _dbContext.SaveChangesAsync();
    //     return Ok();
    // }



    //todo add chapter for book

    // [HttpPost("/chapter/{bookId}")]
    // [Consumes("application/json")]

    // public async Task<IActionResult> PostChapter(int bookId, [FromBody] InsertChapter insertChapter)
    // {
    //     return Ok();
    // }
}
