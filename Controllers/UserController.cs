using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MuseumIstanbul.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MuseumIstanbul.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userManager.Users.ToList();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        return BadRequest(result.Errors);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
    {
        var existingUser = await _userManager.FindByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.UserName = user.UserName;
        existingUser.Email = user.Email;

        var result = await _userManager.UpdateAsync(existingUser);
        if (result.Succeeded)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }
}
