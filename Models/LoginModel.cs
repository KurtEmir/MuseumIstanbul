using System.ComponentModel.DataAnnotations;

namespace MuseumIstanbul.Models;

public class LoginModel
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string UserName { get; set; } = "";

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = "";
}
