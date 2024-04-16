using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Domain
{
    public class DomainContext : DbContext
    {
        public DomainContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<ClubCard> ClubCards { get; set; }

        public DbSet<Busket> Buskets { get; set; }

    }
}
