using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Persistence;

public interface IProductRepository
{
    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
}
