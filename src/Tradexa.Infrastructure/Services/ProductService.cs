using Tradexa.Application.DTOs;
using Tradexa.Application.Interfaces;
using Tradexa.Domain.Entities;
using Tradexa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    //private readonly ILogger<ProductService> _logger;

    public ProductService(ApplicationDbContext context, IMapper mapper)//, ILogger<ProductService> logger)
    {
        _context = context;
        _mapper = mapper;
      //  _logger = logger;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(string language)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Providers)
            .ToListAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            EnglishName = p.EnglishName,
            ArabicName = p.ArabicName,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            //CategoryId = p.CategoryId,
            //CategoryEnglishName = p.Category.EnglishName,
            //CategoryArabicName = p.Category.ArabicName,
            // = p.CreatedAt,
            //UpdatedAt = p.UpdatedAt
        });
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Providers)
            .FirstOrDefaultAsync(p => p.Id == id);

        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<Guid> CreateAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

       // _logger.LogInformation("Product created: {ProductId}", product.Id);
        return product.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _mapper.Map(dto, product);
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

       // _logger.LogInformation("Product updated: {ProductId}", id);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

      //  _logger.LogInformation("Product deleted: {ProductId}", id);
        return true;
    }

    public Task<bool> RestockAsync(Guid productId, int quantity)
    {
        throw new NotImplementedException();
    }
}
