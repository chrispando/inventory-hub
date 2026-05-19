using System.Net.Http.Json;
using ClientApp.Models;

namespace ClientApp.Services;

public class ProductService(HttpClient http)
{
    private Product[]? cachedProducts;

    public async Task<Product[]> GetProductsAsync()
    {
        if (cachedProducts is not null)
        {
            return cachedProducts;
        }

        cachedProducts = await http.GetFromJsonAsync<Product[]>("http://localhost:5026/api/products")
            ?? Array.Empty<Product>();

        return cachedProducts;
    }
}
