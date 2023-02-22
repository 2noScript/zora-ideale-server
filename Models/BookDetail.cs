using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NoScrip.Models;

[Table("BookDetail")]
public class BookDetail
{
    [Key]
    public int BookDetailId { get; set; }
    public BookStatus BookStatus { get; set; }

    // [Required]
    [StringLength(250)]
    public string? Description { get; set; }


    [StringLength(150)]

    public string? Author { get; set; }


    public virtual Book? Book { get; set; }

    public virtual Star? Star { get; set; }



    public virtual ICollection<BookGenres>? BookGenres { get; set; }


    public virtual ICollection<BookAvatar>? BookAvatars { get; set; }
    public virtual ICollection<BookChapter>? BookChapter { get; set; }








}
