using System.ComponentModel.DataAnnotations;

namespace MuseumIstanbul.Models;

// Models/RegisterModel.cs


public class RegisterModel
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string UserName { get; set; } = "";

    [Required]
    [EmailAddress]
    [StringLength(100, MinimumLength = 5)]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = "";

    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string FullName { get; set; } = "";
}



