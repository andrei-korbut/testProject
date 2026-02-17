using Microsoft.AspNetCore.Mvc;
using ProductManagementApi.DTOs;
using ProductManagementApi.Services;

namespace ProductManagementApi.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                });
            }

            var createdProduct = await _productService.CreateProductAsync(dto);

            return CreatedAtAction(
                nameof(CreateProduct),
                new { id = createdProduct.Id },
                createdProduct
            );
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Validation error during product creation");
            return BadRequest(new
            {
                message = ex.Message,
                errors = new Dictionary<string, string[]>
                {
                    { "Name", new[] { ex.Message } }
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            return StatusCode(500, new
            {
                message = "An unexpected error occurred while creating the product"
            });
        }
    }
}
