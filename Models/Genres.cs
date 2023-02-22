using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoScrip.Models;


[Table("Genres")]
public class Genres
{
    [Key]
    public int GenresId { get; set; }

    [StringLength(100)]
    [Required]
    public string? Tag { get; set; }

}