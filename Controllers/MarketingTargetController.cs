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

        // GET: api/MarketingTarget
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketingTarget>>> GetAllTargets()
        {
            return await _context.MarketingTargets.ToListAsync();
        }

        // GET: api/MarketingTarget/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarketingTarget>> GetTargetById(int id)
        {
            var target = await _context.MarketingTargets.FindAsync(id);

            if (target == null)
            {
                return NotFound();
            }

            return target;
        }

        // POST: api/MarketingTarget
        [HttpPost]
        public async Task<ActionResult<MarketingTarget>> CreateTarget(MarketingTarget target)
        {
            _context.MarketingTargets.Add(target);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTargetById), new { id = target.Id }, target);
        }

        // PUT: api/MarketingTarget/5
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
                if (!_context.MarketingTargets.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/MarketingTarget/5
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
    }
}
