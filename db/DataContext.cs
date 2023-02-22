using Microsoft.EntityFrameworkCore;





using NoScrip.Models;
namespace NoScrip.Db;


public class DataContext : DbContext
{

    public DbSet<Genres> Genres { get; set; }

    public DbSet<Server> Server { get; set; }

    public DbSet<Book> Book { get; set; }
    public DbSet<BookDetail> BookDetail { get; set; }
    public DbSet<BookAvatar> BookAvatar { get; set; }
    public DbSet<BookGenres> BookGenres { get; set; }
    public DbSet<BookChapter> bookChapter { get; set; }

    public DbSet<BookChapterServer> BookChapterServer { get; set; }

    public DbSet<Image> Image { get; set; }

    public DbSet<View> View { get; set; }
    public DbSet<Star> Star { get; set; }
    public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
   {
       builder
                  .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning)
                  .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Debug)
                  .AddConsole();
   });
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        base.OnConfiguring(optionsBuilder);


        // optionsBuilder.UseLoggerFactory(loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookDetail>(e =>
        {
            e.HasOne(bd => bd.Book)
            .WithOne(b => b.BookDetail)
            .HasForeignKey<BookDetail>(bd => bd.BookDetailId)
            .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(db => db.Star)
            .WithOne(s => s.BookDetail)
            .HasForeignKey<Star>(s => s.StarId)
            .OnDelete(DeleteBehavior.Cascade);

        });


        modelBuilder.Entity<Server>(e =>
      {

          e.HasIndex(s => s.Domain).IsUnique();

      });

        modelBuilder.Entity<Genres>(e =>
        {

            e.HasIndex(g => g.Tag).IsUnique();

        });


        modelBuilder.Entity<BookGenres>(e =>
        {

            e.HasKey(e => new
            {
                e.BookDetailId,
                e.GenresId
            });

        });

        modelBuilder.Entity<BookAvatar>(e =>
        {

            e.HasKey(e => new
            {
                e.BookDetailId,
                e.ServerId,
                e.Url
            });

        });

        modelBuilder.Entity<BookChapter>(e =>
         {

             e.HasIndex(e => new
             {
                 e.Count,
                 e.BookDetailId
             }).IsUnique();

         });
        modelBuilder.Entity<BookChapterServer>(e =>
         {

             e.HasIndex(bcs => new
             {
                 bcs.ServerId,
                 bcs.BookChapterId
             }).IsUnique();

         });
        modelBuilder.Entity<Image>(e =>
         {

             e.HasIndex(e => new
             {
                 e.BookChapterServerId,
                 e.index
             }).IsUnique();

         });


    }
}