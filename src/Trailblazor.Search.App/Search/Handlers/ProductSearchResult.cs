using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Search.Handlers;

public sealed class ProductSearchResult(Product product) : SearchResult
{
    public Product Product { get; } = product;
    public override object Key => Product.Id;
}
