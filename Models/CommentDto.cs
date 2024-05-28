using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MuseumIstanbul.Models
{
    public class CommentDto
    {
        [Required(ErrorMessage = "UserComment is required")]
        public string UserComment { get; set; }

        [Required(ErrorMessage = "MuseumId is required")]
        public int MuseumId { get; set; }

        // Bu alanlar JSON serileştirmesinden hariç tutulacak
        [JsonIgnore]
        public string UserName { get; set; } = "";

        [JsonIgnore]
        public DateTime RegisterDate { get; set; }
        [JsonIgnore]

        public string UserId { get; set; } = "";
    }
}
