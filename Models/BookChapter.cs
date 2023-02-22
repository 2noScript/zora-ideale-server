
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NoScrip.Models;


[Table("BookChapter")]
public class BookChapter
{


    [Key]
    public int BookChapterId { get; set; }

    public int BookDetailId { get; set; }

    [Required]
    [Column(TypeName = "float")]
    public float Count { get; set; }

    [Required]
    [ForeignKey("BookDetailId")]
    public virtual BookDetail? BookDetail { get; set; }


}
