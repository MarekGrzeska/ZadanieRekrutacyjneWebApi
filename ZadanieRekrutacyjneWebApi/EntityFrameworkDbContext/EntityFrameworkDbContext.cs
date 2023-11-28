using Microsoft.EntityFrameworkCore;
using ZadanieRekrutacyjneWebApi.Entities;

namespace ZadanieRekrutacyjneWebApi.EntityFrameworkDbContext
{
    // entity framework używam tylko do utworzenia bazy danych w modelu code-first
    // sama komunikacją z utworzoną już bazą danych odbywa się za pomocą dappera zgodnie z zaleceniem
    public class EntityFrameworkDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Price> Prices { get; set; }

        public EntityFrameworkDbContext(DbContextOptions<EntityFrameworkDbContext> options) : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(eb =>
            {
                eb.HasKey(p => p.SKU);
                eb.Property(p => p.SKU).IsRequired();
            });
            modelBuilder.Entity<Inventory>().Property(i => i.sku).IsRequired();
            modelBuilder.Entity<Price>().Property(i => i.SKU).IsRequired();
        }
    }
}
