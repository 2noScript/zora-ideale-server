using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NoScrip.Models;

[Table("Star")]
public class Star
{

    [Key]
    public int StarId { get; set; }

    [ForeignKey("StarId")]
    [Required]
    public virtual BookDetail? BookDetail { get; set; }


    public int S1 { get; set; } = 0;
    public int S2 { get; set; } = 0;
    public int S3 { get; set; } = 0;
    public int S4 { get; set; } = 0;
    public int S5 { get; set; } = 0;

}