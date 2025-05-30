using Microsoft.EntityFrameworkCore;
using MyBackendApi.Models;

namespace MyBackendApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Negotiation> BusinessNegotiations { get; set; }
        public DbSet<IssuedQuotations> IssuedQotations { get; set; }
        public DbSet<QuotationClearance> QuotationClearances { get; set; }
        public DbSet<ApprovedClearance> ApprovedClearances { get; set; }
        public DbSet<MarketingTarget> MarketingTargets { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
    }

}