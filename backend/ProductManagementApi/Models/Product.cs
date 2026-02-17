using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public ProductType Type { get; set; }

    public DateTime CreatedAt { get; set; }
}
