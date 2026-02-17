using ProductManagementApi.DTOs;
using ProductManagementApi.Models;
using ProductManagementApi.Repositories;

namespace ProductManagementApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        // Validate name uniqueness
        var nameExists = await _productRepository.ProductExistsByNameAsync(dto.Name);
        if (nameExists)
        {
            throw new InvalidOperationException("A product with this name already exists");
        }

        // Map DTO to entity
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Type = dto.Type!.Value,
            CreatedAt = DateTime.UtcNow
        };

        // Save to database
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        // Map entity to DTO
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Type = product.Type.ToString(),
            CreatedAt = product.CreatedAt
        };
    }
}
