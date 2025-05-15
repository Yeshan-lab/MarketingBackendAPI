using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;
using MyBackendApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingOfficerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarketingOfficerController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/MarketingOfficer
        [HttpPost]
        public async Task<ActionResult<MarketingOfficer>> PostMarketingOfficer(MarketingOfficer marketingOfficer)
        {
            _context.MarketingOfficers.Add(marketingOfficer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarketingOfficer", new { id = marketingOfficer.Id }, marketingOfficer);
        }

        // GET: api/MarketingOfficer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketingOfficer>>> GetMarketingOfficers()
        {
            return await _context.MarketingOfficers.ToListAsync();
        }

        // GET: api/MarketingOfficer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarketingOfficer>> GetMarketingOfficer(int id)
        {
            var marketingOfficer = await _context.MarketingOfficers.FindAsync(id);

            if (marketingOfficer == null)
            {
                return NotFound();
            }

            return marketingOfficer;
        }

        // PUT: api/MarketingOfficer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarketingOfficer(int id, MarketingOfficer marketingOfficer)
        {
            if (id != marketingOfficer.Id)
            {
                return BadRequest();
            }

            _context.Entry(marketingOfficer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarketingOfficerExists(id))
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

        // DELETE: api/MarketingOfficer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarketingOfficer(int id)
        {
            var marketingOfficer = await _context.MarketingOfficers.FindAsync(id);
            if (marketingOfficer == null)
            {
                return NotFound();
            }

            _context.MarketingOfficers.Remove(marketingOfficer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarketingOfficerExists(int id)
        {
            return _context.MarketingOfficers.Any(e => e.Id == id);
        }
    }
}
