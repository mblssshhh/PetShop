using Microsoft.EntityFrameworkCore;
using PetShopWeb.Data.Entity;


namespace PetShopWeb.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Busket> Buskets { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<ClubCard> ClubCards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ClubCard>()
                .Property(c => c.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>()
                .HasMany(c => c.ProductCategories)
                .WithOne(pc => pc.Category)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.ProductCategories)
                .WithOne(pc => pc.Category)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Buskets)
                .WithOne(b => b.Product)
                .HasForeignKey(b => b.ProductId);

            modelBuilder.Entity<Buyer>()
                .HasMany(b => b.Buskets)
                .WithOne(b => b.Buyer)
                .HasForeignKey(b => b.BuyerId);

            modelBuilder.Entity<Buyer>()
                .HasMany(b => b.ClubCards)
                .WithOne(cc => cc.Buyer)
                .HasForeignKey(cc => cc.BuyerId);

            modelBuilder.Entity<Busket>()
                .HasMany(b => b.Orders)
                .WithOne(o => o.Busket)
                .HasForeignKey(o => o.BasketId);

            modelBuilder.Entity<Staff>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.Staff)
                .HasForeignKey(o => o.StaffId);
        }
    }
}
