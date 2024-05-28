using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseumIstanbul.Data;
using MuseumIstanbul.Models;

namespace MuseumIstanbul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuseumsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public MuseumsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Museums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Museum>>> GetMuseums()
        {
            if (_context.Museums == null)
            {
                return NotFound();
            }

            var museums = await _context.Museums
                                        .Include(m => m.Comments)
                                        .Select(m => new MuseumDto
                                        {
                                            Id = m.Id,
                                            Name = m.Name,
                                            Type = m.Type,
                                            Location = m.Location,
                                            Description = m.Description,
                                            OpeningTime = m.OpeningTime,
                                            ClosingTime = m.ClosingTime,
                                            Email = m.Email,
                                            PhoneNumber = m.PhoneNumber,
                                            Price = m.Price,
                                            IsDeleted = m.IsDeleted,
                                            RegisterDate = m.RegisterDate,
                                            PhotoUrl = m.PhotoUrl,
                                            Comments = m.Comments.Select(c => new CommentDto
                                            {
                                                MuseumId = m.Id,
                                                UserComment = c.UserComment,
                                            }).ToList()
                                        })
                                        .ToListAsync();

            return Ok(museums);
        }

        // GET: api/Museums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MuseumDto>> GetMuseum(int id)
        {
            if (_context.Museums == null)
            {
                return NotFound();
            }

            var museum = await _context.Museums
                                       .Include(m => m.Comments)
                                       .FirstOrDefaultAsync(m => m.Id == id);

            if (museum == null)
            {
                return NotFound();
            }

            var museumDto = new MuseumDto
            {
                Id = museum.Id,
                Name = museum.Name,
                Type = museum.Type,
                Location = museum.Location,
                Description = museum.Description,
                OpeningTime = museum.OpeningTime,
                ClosingTime = museum.ClosingTime,
                Email = museum.Email,
                PhoneNumber = museum.PhoneNumber,
                Price = museum.Price,
                IsDeleted = museum.IsDeleted,
                RegisterDate = museum.RegisterDate,
                PhotoUrl = museum.PhotoUrl,
                Comments = museum.Comments.Select(c => new CommentDto
                {
                    MuseumId = museum.Id,
                    UserComment = c.UserComment,
                    UserName = c.Username,
                    RegisterDate = c.CreatedAt,
                }).ToList()
            };

            return museumDto;
        }

        // PUT: api/Museums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMuseum(int id, Museum museum)
        {
            if (id != museum.Id)
            {
                return BadRequest();
            }

            _context.Entry(museum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MuseumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Museums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Museum>> PostMuseum(Museum museum)
        {
          if (_context.Museums == null)
          {
              return Problem("Entity set 'ApplicationContext.Museums'  is null.");
          }
            _context.Museums.Add(museum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMuseum", new { id = museum.Id }, museum);
        }

        // DELETE: api/Museums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMuseum(int id)
        {
            if (_context.Museums == null)
            {
                return NotFound();
            }
            var museum = await _context.Museums.FindAsync(id);
            if (museum == null)
            {
                return NotFound();
            }

            _context.Museums.Remove(museum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MuseumExists(int id)
        {
            return (_context.Museums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
