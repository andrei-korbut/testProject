using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (!string.IsNullOrEmpty(dbPassword))
{
    connectionString = connectionString?.Replace("${DB_PASSWORD}", dbPassword);
}

// Log connection info (masked)
var maskedConnectionString = connectionString?.Replace(dbPassword ?? "", "***");
Console.WriteLine($"Using connection string: {maskedConnectionString}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Apply migrations automatically with retry logic
await ApplyMigrationsAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health check endpoint
app.MapGet("/health", async (AppDbContext dbContext) =>
{
    try
    {
        // Test database connectivity
        await dbContext.Database.CanConnectAsync();
        
        return Results.Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow.ToString("o")
        });
    }
    catch (Exception ex)
    {
        return Results.Json(new
        {
            status = "unhealthy",
            timestamp = DateTime.UtcNow.ToString("o"),
            error = ex.Message
        }, statusCode: 503);
    }
})
.WithName("HealthCheck")
.WithOpenApi();

app.Run();

// Helper method to apply migrations with retry logic
static async Task ApplyMigrationsAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<AppDbContext>();

    const int maxRetries = 10;
    const int delaySeconds = 10;

    for (int retry = 1; retry <= maxRetries; retry++)
    {
        try
        {
            logger.LogInformation("Starting database migration... (Attempt {Retry}/{MaxRetries})", retry, maxRetries);
            
            await context.Database.MigrateAsync();
            
            logger.LogInformation("Database migration completed successfully.");
            return;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error applying database migrations (Attempt {Retry}/{MaxRetries})", retry, maxRetries);
            
            if (retry == maxRetries)
            {
                logger.LogCritical("Failed to apply database migrations after {MaxRetries} attempts. Application will exit.", maxRetries);
                throw;
            }
            
            logger.LogInformation("Waiting {DelaySeconds} seconds before retry...", delaySeconds);
            await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
        }
    }
}

