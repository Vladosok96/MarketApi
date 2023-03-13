using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace MarketApi.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleData> SalesData { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MarketDB;Username=postgres;Password=PostgresTest");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // many-to-many relation with extra parameter (productQuantity) between product and sales point
            modelBuilder.Entity<ProvidedProduct>()
                .HasOne<Product>(pp => pp.Product)
                .WithMany(p => p.ProvidedProducts)
                .HasForeignKey(pp => pp.ProductId);
            modelBuilder.Entity<ProvidedProduct>()
                .HasOne<SalesPoint>(pp => pp.SalesPoint)
                .WithMany(sp => sp.ProvidedProducts)
                .HasForeignKey(pp => pp.SalesPointId);

            modelBuilder.Entity<Sale>()
                .HasOne<Buyer>(s => s.Buyer)
                .WithMany(b => b.SalesIds)
                .HasForeignKey(s => s.BuyerId)
                .IsRequired(false);

            // many-to-many relation with extra parameter (productQuantity, amount) between product and sales
            modelBuilder.Entity<SaleData>()
                .HasOne<Product>(sd => sd.Product)
                .WithMany(p => p.SalesData)
                .HasForeignKey(sd => sd.ProductId);
            modelBuilder.Entity<SaleData>()
                .HasOne<Sale>(sd => sd.Sale)
                .WithMany(s => s.SalesData)
                .HasForeignKey(sd => sd.SaleId);
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
