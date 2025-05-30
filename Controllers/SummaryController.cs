// SummaryController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;
using MyBackendApi.Models;

namespace MyBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummaryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SummaryController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Officer Summary: ApprovedClearance totals for a given month/year
        [HttpGet("officer-summary/{username}/{month}/{year}")]
        public async Task<ActionResult<object>> GetOfficerSummary(string username, int month, int year)
        {
            var approved = await _context.ApprovedClearances
                .Where(a => a.Username == username && a.Date.Month == month && a.Date.Year == year)
                .ToListAsync();

            decimal totalValue = approved.Sum(a => a.Value);
            decimal totalKW = approved.Sum(a => a.CapacityKW);

            return Ok(new
            {
                Username = username,
                Month = month,
                Year = year,
                TotalValue = totalValue,
                TotalKW = totalKW
            });
        }

        // ✅ Admin Summary: View all officers' progress for current month
        [HttpGet("admin-summary/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAdminSummary(int month, int year)
        {
            var targets = await _context.MarketingTargets
                .Where(t => t.Month == month && t.Year == year)
                .ToListAsync();

            var allClearances = await _context.ApprovedClearances
                .Where(a => a.Date.Month == month && a.Date.Year == year)
                .ToListAsync();

            var summary = targets.Select(t =>
            {
                var officerClearances = allClearances.Where(a => a.Username == t.Username).ToList();

                return new
                {
                    Username = t.Username,
                    TargetValue = t.ValueTarget,
                    TargetKW = t.KWTarget,
                    ApprovedValue = officerClearances.Sum(c => c.Value),
                    ApprovedKW = officerClearances.Sum(c => c.CapacityKW),
                    RemainingValue = t.ValueTarget - officerClearances.Sum(c => c.Value),
                    RemainingKW = t.KWTarget - officerClearances.Sum(c => c.CapacityKW)
                };
            });

            return Ok(summary);
        }
    }
}
