using ProductManagementApi.DTOs;

namespace ProductManagementApi.Services;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
}
