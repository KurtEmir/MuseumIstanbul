using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseumIstanbul.Data;
using MuseumIstanbul.Models;

namespace MuseumIstanbul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public CommentsController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            comment = new Comment
            {
                UserComment = comment.UserComment,
                Username = comment.Username,
                CreatedAt = comment.RegisterDate,
                MuseumId = comment.MuseumId
            };

            return comment;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment([FromBody] CommentDto commentDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized("User not found");
            }
            // Set the UserName and RegisterDate internally
            commentDto.UserName = user.UserName;
            commentDto.RegisterDate = DateTime.UtcNow;
            commentDto.UserId = userId;

            if (string.IsNullOrWhiteSpace(commentDto.UserComment))
            {
                ModelState.AddModelError("UserComment", "UserComment is required");
            }

            if (commentDto.MuseumId <= 0)
            {
                ModelState.AddModelError("MuseumId", "MuseumId must be a positive number");
            }

            try
            {
                var comment = new Comment
                {
                    UserId = userId,
                    UserComment = commentDto.UserComment,
                    Username = commentDto.UserName,
                    CreatedAt = commentDto.RegisterDate,
                    MuseumId = commentDto.MuseumId
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error posting comment: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("museum/{museumId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByMuseumId(int museumId)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                                         .Where(c => c.MuseumId == museumId)
                                         .Select(c => new
                                         {
                                             c.Username,
                                             c.CreatedAt,
                                             c.UserComment,
                                             c.MuseumId
                                         })
                                         .ToListAsync();

            if (comments == null || comments.Count == 0)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        private bool CommentExists(int id)
        {
            return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
