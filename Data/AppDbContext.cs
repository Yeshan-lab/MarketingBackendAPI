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
        public DbSet<MarketingOfficer> MarketingOfficers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLogin>()
    .HasOne(u => u.Admin)
    .WithMany(a => a.UserLogins)
    .HasForeignKey(u => u.AdminId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserLogin>()
                .HasOne(u => u.Officer)
                .WithMany(o => o.UserLogins)
                .HasForeignKey(u => u.OfficerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
