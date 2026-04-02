using PricesService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PricesService.Data;

public class PricesServiceDbContext(DbContextOptions<PricesServiceDbContext> options) : DbContext(options)
{
    public DbSet<PriceEntity> Prices => Set<PriceEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceEntity>(entity =>
        {
            entity.ToTable("prices");
            entity.HasKey(price => price.Id);

            entity.Property(price => price.Id)
                .HasColumnName("id")
                .HasColumnType("varchar");

            entity.Property(price => price.ProductId)
                .HasColumnName("product_id")
                .HasColumnType("varchar")
                .IsRequired();

            entity.Property(price => price.StoreId)
                .HasColumnName("store_id")
                .HasColumnType("varchar")
                .IsRequired();

            entity.Property(price => price.Price)
                .HasColumnName("price")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            entity.Property(price => price.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            // Unique constraint: one price per product+store pair
            entity.HasIndex(price => new { price.ProductId, price.StoreId })
                .IsUnique()
                .HasDatabaseName("UX_prices_product_store");

            entity.HasIndex(price => price.ProductId)
                .HasDatabaseName("IX_prices_product_id");
        });
    }
}
