using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoScrip.Models;

[Table("Image")]
public class Image
{

    [Key]
    public int ImageId { get; set; }


    public int BookChapterServerId { get; set; }


    [Required]
    [ForeignKey("BookChapterServerId")]
    public virtual BookChapterServer? BookChapterServer { get; set; }

    [StringLength(250)]
    [Required]
    public string? Url { get; set; }

    [Required]
    public int index { get; set; }


}

