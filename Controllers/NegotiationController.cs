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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Negotiation>>> GetAll()
        {
            return await _context.BusinessNegotiations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Negotiation>> Get(int id)
        {
            var negotiation = await _context.BusinessNegotiations.FindAsync(id);
            if (negotiation == null)
                return NotFound();

            return negotiation;
        }

        [HttpPost]
        public async Task<ActionResult<Negotiation>> Create(Negotiation negotiation)
        {
            _context.BusinessNegotiations.Add(negotiation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = negotiation.Id }, negotiation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Negotiation negotiation)
        {
            if (id != negotiation.Id)
                return BadRequest();

            _context.Entry(negotiation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

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
