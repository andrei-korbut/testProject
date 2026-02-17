using System.ComponentModel.DataAnnotations;
using ProductManagementApi.Models;

namespace ProductManagementApi.DTOs;

public class CreateProductDto
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product description is required")]
    [MaxLength(1000, ErrorMessage = "Product description cannot exceed 1000 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product type is required")]
    public ProductType? Type { get; set; }
}
