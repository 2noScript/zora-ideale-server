using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoScrip.Models;

[Table("BookGenres")]


public class BookGenres
{

    public int BookDetailId { get; set; }

    public int GenresId { get; set; }



    [Required]
    [ForeignKey("BookDetailId")]
    public virtual BookDetail? BookDetail { get; set; }

    [Required]
    [ForeignKey("GenresId")]
    public virtual Genres? Genres { get; set; }

}

