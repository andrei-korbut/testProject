using ProductManagementApi.Models;

namespace ProductManagementApi.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> ProductExistsByNameAsync(string name);
}
