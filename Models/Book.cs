using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using System.ComponentModel;
namespace NoScrip.Models;

[Table("Book")]
public class Book
{
    [Key]
    public int BookId { get; set; }

    [StringLength(250)]
    [Column(TypeName = "varchar")]
    [Required]
    public string? Name { get; set; }
    public DateTime Create_at { get; set; } = DateTime.Now;
    public DateTime Update_at { get; set; } = DateTime.Now;
    public virtual BookDetail? BookDetail { get; set; }

}

