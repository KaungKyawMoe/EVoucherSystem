
using EVoucherSystem.Models;
using Microsoft.EntityFrameworkCore;
using EVoucherSystem.Dtos;
namespace EVoucherSystem
{
    public class EVoucherDbContext : DbContext
    {
        public EVoucherDbContext(DbContextOptions<EVoucherDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<EVoucher> EVouchers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EVoucher>().ToTable("Evouchers");
            modelBuilder.Entity<Purchase>().ToTable("Purchase");
        }

        public DbSet<EVoucherSystem.Models.PurchaseEVoucherViewModel>? PurchaseEVoucherViewModel { get; set; }

        public DbSet<EVoucherSystem.Dtos.EVoucherDto>? EVoucherDto { get; set; }
    }
}
