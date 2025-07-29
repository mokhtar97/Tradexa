using Tradexa.Application.DTOs;

namespace Tradexa.Application.Interfaces;

public interface IProductService
{
    Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParameters query);
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(ProductCreateDto dto);
    Task<bool> UpdateAsync(Guid id, ProductUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestockAsync(Guid productId, int quantity);
}
