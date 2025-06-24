using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;
using MyBackendApi.Models;

namespace MyBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuedQuotationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IssuedQuotationController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all quotations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssuedQuotations>>> GetAll()
        {
            return await _context.IssuedQotations.ToListAsync();
        }

        // ✅ Get quotations by username
        [HttpGet("by-user/{username}")]
        public async Task<ActionResult<IEnumerable<IssuedQuotations>>> GetByUser(string username)
        {
            var data = await _context.IssuedQotations
                .Where(q => q.Username == username)
                .ToListAsync();

            return data;
        }

        // ✅ Get a specific quotation by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<IssuedQuotations>> Get(int id)
        {
            var quotation = await _context.IssuedQotations.FindAsync(id);
            if (quotation == null)
                return NotFound();

            return quotation;
        }

        // ✅ Create a new quotation
        [HttpPost]
        public async Task<ActionResult<IssuedQuotations>> Create(IssuedQuotations quotation)
        {
            quotation.Date = DateTime.UtcNow;
            _context.IssuedQotations.Add(quotation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = quotation.Id }, quotation);
        }

        // ✅ Update an existing quotation
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IssuedQuotations quotation)
        {
            if (id != quotation.Id)
                return BadRequest();

            quotation.Date = quotation.Date.ToUniversalTime();

            _context.Entry(quotation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ Delete a quotation
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var quotation = await _context.IssuedQotations.FindAsync(id);
            if (quotation == null)
                return NotFound();

            _context.IssuedQotations.Remove(quotation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}