using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;
using MyBackendApi.Models;

namespace MyBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegotiationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NegotiationController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all negotiations (admin only)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Negotiation>>> GetAll()
        {
            return await _context.BusinessNegotiations.ToListAsync();
        }

        // ✅ Get all negotiations by username (officer or admin)
        [HttpGet("by-user/{username}")]
        public async Task<ActionResult<IEnumerable<Negotiation>>> GetByUsername(string username)
        {
            return await _context.BusinessNegotiations
                .Where(n => n.Username == username)
                .ToListAsync();
        }

        // ✅ Get single record
        [HttpGet("{id}")]
        public async Task<ActionResult<Negotiation>> Get(int id)
        {
            var negotiation = await _context.BusinessNegotiations.FindAsync(id);
            if (negotiation == null)
                return NotFound();

            return negotiation;
        }

        // ✅ Create new negotiation (officer + admin)
        [HttpPost]
        public async Task<ActionResult<Negotiation>> Create(Negotiation negotiation)
        {
            negotiation.Date = DateTime.UtcNow;
            _context.BusinessNegotiations.Add(negotiation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = negotiation.Id }, negotiation);
        }

        // ✅ Update (admin only)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Negotiation negotiation)
        {
            if (id != negotiation.Id)
                return BadRequest();

            negotiation.Date = negotiation.Date.ToUniversalTime();

            _context.Entry(negotiation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ Delete (admin only)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var negotiation = await _context.BusinessNegotiations.FindAsync(id);
            if (negotiation == null)
                return NotFound();

            _context.BusinessNegotiations.Remove(negotiation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}