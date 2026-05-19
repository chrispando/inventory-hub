using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy
            .WithOrigins("https://localhost:5022", "http://localhost:5022")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowBlazorClient");

Product[] GetProducts(IMemoryCache cache)
{
    return cache.GetOrCreate("products", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

        return new[]
        {
            new Product(1, "Laptop", 1200.50, 25),
            new Product(2, "Headphones", 50.00, 100)
        };
    }) ?? [];
}

app.MapGet("/api/products", GetProducts);

app.MapGet("/api/productlist", GetProducts);

app.Run();

public record Product(int Id, string Name, double Price, int Stock);
