using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradexa.Application.Interfaces;
using Tradexa.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace Tradexa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var language = Request.Headers["Accept-Language"].ToString() ?? "en";
        _logger.LogInformation("Fetching all products for language: {Language}", language);

        var products = await _productService.GetAllAsync(language);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            _logger.LogWarning("Product not found: {ProductId}", id);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid product creation request");
            return BadRequest(ModelState);
        }

        var newId = await _productService.CreateAsync(dto);
        _logger.LogInformation("Product created with ID: {ProductId}", newId);

        return CreatedAtAction(nameof(Get), new { id = newId }, newId);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto dto)
    {
        var updated = await _productService.UpdateAsync(id, dto);
        if (!updated)
        {
            _logger.LogWarning("Failed to update product with ID: {ProductId}", id);
            return NotFound();
        }

        _logger.LogInformation("Product updated: {ProductId}", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _productService.DeleteAsync(id);
        if (!deleted)
        {
            _logger.LogWarning("Failed to delete product with ID: {ProductId}", id);
            return NotFound();
        }

        _logger.LogInformation("Product deleted: {ProductId}", id);
        return NoContent();
    }
}
