using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuseumIstanbul.Models;

public class Museum 
{
    [Key]
    public int Id { get; set; }

    [StringLength(100, MinimumLength = 2)]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = "";
    public MuseumType Type { get; set; }
    [StringLength(350, MinimumLength = 2)]
    [Column(TypeName = "nvarchar(350)")]
    public string Location { get; set; } = "";
    [StringLength(2000, MinimumLength = 2)]
    [Column(TypeName = "nvarchar(2000)")]
    public string Description { get; set; } = "";
    public TimeSpan OpeningTime { get; set; } 
    public TimeSpan ClosingTime { get; set; }

    [EmailAddress]
    [StringLength(100, MinimumLength = 5)]
    [Column(TypeName = "varchar(100)")]
    public string Email { get; set; } = "";

    [Phone]
    [StringLength(30)]
    [Column(TypeName = "varchar(30)")]
    public string PhoneNumber { get; set; } = "";

    [Range(0, int.MaxValue)]
    public int? Price { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime RegisterDate { get; set; }
    public List<Comment>? Comments { get; set; }

    public string PhotoUrl { get; set; } // Fotoğraf URL'si


    //    {
    //  "name": "Modern Art Museum",
    //  "type": 0,
    //  "location": "Downtown",
    //  "description": "A museum showcasing modern art.",
    //  "openingTime": "09:00:00",
    //  "closingTime": "18:00:00",
    //  "email": "info@modernartmuseum.com",
    //  "phoneNumber": "123-456-7890",
    //  "price": 20,
    //  "isDeleted": false
    //}


}
