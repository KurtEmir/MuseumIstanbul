using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuseumIstanbul.Models;

public class User : IdentityUser
{
    [StringLength(100, MinimumLength = 2)]
    [Column(TypeName = "nvarchar(100)")]
    public override string UserName { get; set; } = "";

    [StringLength(200, MinimumLength = 5)]
    [Column(TypeName = "nvarchar(100)")]
    public string FullName { get; set; } = "";

    [EmailAddress]
    [StringLength(100, MinimumLength = 5)]
    [Column(TypeName = "varchar(100)")]
    public override string Email { get; set; } = "";
    [Phone]
    [StringLength(30)]
    [Column(TypeName = "varchar(30)")]
    public override string? PhoneNumber { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime RegisterDate { get; set; }
}
