using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Tradexa.Domain.Entities;

namespace Tradexa.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Product> Products;
    public DbSet<Category> Categories;
    public DbSet<Provider> Providers;
    public DbSet<ProductProviderPrice> ProductProviderPrices;

    public DbSet<Invoice> Invoices;
    public DbSet<InvoiceItem> InvoiceItems;

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
