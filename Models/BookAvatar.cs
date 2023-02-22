using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NoScrip.Models;

[Table("BookAvatar")]
public class BookAvatar
{

    public int BookDetailId { get; set; }
    public int ServerId { get; set; }

    [ForeignKey("ServerId")]
    [Required]

    public virtual Server? Server { get; set; }

    [ForeignKey("BookDetailId")]
    [Required]
    public virtual BookDetail? BookDetail { get; set; }

    [StringLength(250)]
    [Required]
    public string? Url { get; set; }
}

