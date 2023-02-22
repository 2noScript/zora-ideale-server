using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NoScrip.Models;
[Table("BookChapterServer")]
public class BookChapterServer
{
    [Key]
    public int BookChapterServerId { get; set; }


    public int BookChapterId { get; set; }
    public int ServerId { get; set; }

    [ForeignKey("ServerId")]
    [Required]
    public virtual Server? Server { get; set; }

    [ForeignKey("BookChapterId")]
    [Required]
    public virtual BookChapter? BookChapter { get; set; }

}
