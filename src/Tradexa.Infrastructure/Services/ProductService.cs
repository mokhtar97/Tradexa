using Tradexa.Application.DTOs;
using Tradexa.Application.Interfaces;
using Tradexa.Domain.Entities;
using Tradexa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Tradexa.Infrastructure.Services
{
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

        public async Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParameters query)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                productsQuery = productsQuery.Where(p =>
                    p.EnglishName.Contains(query.Search) ||
                    p.ArabicName.Contains(query.Search) ||
                     p.Description.Contains(query.Search) ||
                    (p.Category != null && (
                        p.Category.EnglishName.Contains(query.Search) ||
                        p.Category.ArabicName.Contains(query.Search))
                    )
                );
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                var isDesc = query.Descending;
                productsQuery = query.SortBy.ToLower() switch
                {
                    "englishname" => isDesc ? productsQuery.OrderByDescending(p => p.EnglishName) : productsQuery.OrderBy(p => p.EnglishName),
                    "arabicname" => isDesc ? productsQuery.OrderByDescending(p => p.ArabicName) : productsQuery.OrderBy(p => p.ArabicName),
                    "price" => isDesc ? productsQuery.OrderByDescending(p => p.Price) : productsQuery.OrderBy(p => p.Price),
                    "stock" => isDesc ? productsQuery.OrderByDescending(p => p.Stock) : productsQuery.OrderBy(p => p.Stock),
                    _ => productsQuery
                };
            }

            var totalCount = await productsQuery.CountAsync();

            var items = await productsQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    EnglishName = p.EnglishName,
                    ArabicName = p.ArabicName,
                    CategoryName = p.Category != null ? p.Category.EnglishName : null,
                    Price = p.Price,
                    Stock = p.Stock
                })
                .ToListAsync();

            return new PaginatedResult<ProductDto>(items, totalCount);
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
}
