using System.Collections.Generic;
using KYC.Models.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace KYC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<KYCM> KYCMs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<VDC> VDCs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascade delete for KYCM foreign keys
            modelBuilder.Entity<KYCM>()
                .HasOne(k => k.Province)
                .WithMany()
                .HasForeignKey(k => k.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KYCM>()
                .HasOne(k => k.District)
                .WithMany()
                .HasForeignKey(k => k.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KYCM>()
                .HasOne(k => k.VDC)
                .WithMany()
                .HasForeignKey(k => k.VDCId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }

}
