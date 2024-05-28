using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MuseumIstanbul.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    public string? UserId { get; set; }
    [JsonIgnore] // This will ignore the UserName field in the request payload
    public string? Username { get; set; } = "";

    public DateTime CreatedAt { get; set; } 
    public int? MuseumId { get; set; }
    [ForeignKey("MuseumId")]
    public Museum? Museum { get; set; }

    [Required] // Ensure this is marked as required
    [StringLength(500, MinimumLength = 2)]
    [Column(TypeName = "nvarchar(500)")]
    public string UserComment { get; set; } = "";
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

}

