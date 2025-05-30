using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;
using MyBackendApi.Models;

namespace MyBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovedClearanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApprovedClearanceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApprovedClearance>>> GetAll()
        {
            return await _context.ApprovedClearances.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApprovedClearance>> Get(int id)
        {
            var clearance = await _context.ApprovedClearances.FindAsync(id);
            if (clearance == null)
                return NotFound();

            return clearance;
        }

        [HttpPost]
        public async Task<ActionResult<ApprovedClearance>> Create(ApprovedClearance clearance)
        {
            _context.ApprovedClearances.Add(clearance);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = clearance.Id }, clearance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ApprovedClearance clearance)
        {
            if (id != clearance.Id)
                return BadRequest();

            _context.Entry(clearance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clearance = await _context.ApprovedClearances.FindAsync(id);
            if (clearance == null)
                return NotFound();

            _context.ApprovedClearances.Remove(clearance);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("by-user/{username}")]
        public async Task<ActionResult<IEnumerable<ApprovedClearance>>> GetByUser(string username)
        {
            var list = await _context.ApprovedClearances
            .Where(c => c.Username == username)
            .ToListAsync();
            return list;

        }
        }
    }
