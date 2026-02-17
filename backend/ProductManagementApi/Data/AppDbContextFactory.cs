using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductManagementApi.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        // Use a dummy connection string for design-time operations
        optionsBuilder.UseSqlServer("Server=localhost;Database=ProductManagement;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;");
        
        return new AppDbContext(optionsBuilder.Options);
    }
}
