using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NoScrip.Models;

[Table("View")]
public class View
{

    [Key]
    public int Id { get; set; }

    public int ChapterID { get; set; }

    [Required]
    [ForeignKey("ChapterID")]
    public virtual BookChapter? BookChapter { get; set; }

    public int count { get; set; } = 0;
}
