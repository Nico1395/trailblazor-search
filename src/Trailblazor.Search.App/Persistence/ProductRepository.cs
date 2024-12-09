using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Persistence;

internal sealed class ProductRepository : IProductRepository
{
    private List<Product>? _products;

    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_products ??= GenerateRandomProducts(100));
    }

    private List<Product> GenerateRandomProducts(int count)
    {
        var random = new Random();
        var now = DateTime.UtcNow;
        var products = new List<Product>();

        for (int i = 0; i < count; i++)
        {
            var createdDate = now.AddDays(-random.Next(0, 365));
            products.Add(new Product
            {
                Name = $"Product {i + 1}",
                Description = $"Description for Product {i + 1}",
                InStock = random.Next(0, 3000),
                Sold = random.Next(0, 40000),
                Created = createdDate,
                LastChanged = createdDate.AddDays(random.Next(0, 30))
            });
        }

        return products;
    }
}
