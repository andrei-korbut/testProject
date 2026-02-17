using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Data;
using ProductManagementApi.Models;

namespace ProductManagementApi.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ProductExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(p => p.Name == name);
    }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }
}
