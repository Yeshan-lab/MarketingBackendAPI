using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;
using MyBackendApi.Models;

namespace MyBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationClearanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuotationClearanceController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationClearance>>> GetAll()
        {
            return await _context.QuotationClearances.ToListAsync();
        }

        // ✅ Get all by username
        [HttpGet("byuser/{username}")]
        public async Task<ActionResult<IEnumerable<QuotationClearance>>> GetByUsername(string username)
        {
            return await _context.QuotationClearances
                .Where(c => c.Username == username)
                .ToListAsync();
        }

        // ✅ Get by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationClearance>> Get(int id)
        {
            var clearance = await _context.QuotationClearances.FindAsync(id);
            if (clearance == null)
                return NotFound();

            return clearance;
        }

        // ✅ Create
        [HttpPost]
        public async Task<ActionResult<QuotationClearance>> Create(QuotationClearance clearance)
        {
            clearance.Date = DateTime.UtcNow;
            _context.QuotationClearances.Add(clearance);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = clearance.Id }, clearance);
        }

        // ✅ Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, QuotationClearance clearance)
        {
            if (id != clearance.Id)
                return BadRequest();

            _context.Entry(clearance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clearance = await _context.QuotationClearances.FindAsync(id);
            if (clearance == null)
                return NotFound();

            _context.QuotationClearances.Remove(clearance);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}