using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Tradexa.Domain.Entities;

namespace Tradexa.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<ProductProviderPrice> ProductProviderPrices { get; set; }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure composite key for ProductProviderPrice
        builder.Entity<ProductProviderPrice>()
            .HasKey(p => new { p.ProductId, p.ProviderId, p.EffectiveDate });

        builder.Entity<Product>()
            .HasMany(p => p.Providers)
            .WithMany(p => p.Products);

        builder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Product>().ToTable("Product");
        builder.Entity<Category>().ToTable("Category");
        builder.Entity<Provider>().ToTable("Provider");
        builder.Entity<ProductProviderPrice>().ToTable("ProductProviderPrice");
        builder.Entity<Invoice>().ToTable("Invoice");
        builder.Entity<InvoiceItem>().ToTable("InvoiceItem");
        builder.Entity<InvoiceItem>()
            .HasOne(i => i.Invoice)
            .WithMany(i => i.Items)
            .HasForeignKey(i => i.InvoiceId);

        builder.Entity<InvoiceItem>()
            .HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId);

        // Add indexes or further configuration as needed
    }
}
