using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoScrip.Models;


[Table("Server")]
public class Server
{
    [Key]
    public int ServerId { get; set; }

    [StringLength(100)]
    [Required]
    public string? Name { get; set; }

    [StringLength(100)]
    [Required]
    public string? Domain { get; set; }

}