using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;
using MyBackendApi.Models;

namespace MyBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketingTargetController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarketingTargetController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all targets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketingTarget>>> GetAllTargets()
        {
            return await _context.MarketingTargets.ToListAsync();
        }

        // ✅ Get target by username and month
        [HttpGet("{username}/{month}/{year}")]
        public async Task<ActionResult<MarketingTarget>> GetTargetByUserAndMonth(string username, int month, int year)
        {
            var target = await _context.MarketingTargets.FirstOrDefaultAsync(t => t.Username == username && t.Month == month && t.Year == year);

            if (target == null)
            {
                return NotFound();
            }

            return target;
        }

        // ✅ Create new target
        [HttpPost]
        public async Task<ActionResult<MarketingTarget>> CreateTarget(MarketingTarget target)
        {
            // Prevent duplicate for same month/year/user
            bool exists = await _context.MarketingTargets.AnyAsync(t => t.Username == target.Username && t.Month == target.Month && t.Year == target.Year);
            if (exists)
            {
                return Conflict(new { message = "Target already submitted for this month." });
            }

            _context.MarketingTargets.Add(target);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTargetByUserAndMonth), new { username = target.Username, month = target.Month, year = target.Year }, target);
        }

        // ✅ Update target
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarget(int id, MarketingTarget target)
        {
            if (id != target.Id)
            {
                return BadRequest();
            }

            _context.Entry(target).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // ✅ Delete target
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarget(int id)
        {
            var target = await _context.MarketingTargets.FindAsync(id);
            if (target == null)
            {
                return NotFound();
            }

            _context.MarketingTargets.Remove(target);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TargetExists(int id)
        {
            return _context.MarketingTargets.Any(t => t.Id == id);
        }
    }
}
