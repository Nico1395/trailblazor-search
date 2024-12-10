using Trailblazor.Search.App.Persistence;
using Trailblazor.Search.Criteria.Extensions;
using Trailblazor.Search.Criteria.Workers;

namespace Trailblazor.Search.App.Search.Handlers;

internal sealed class ProductSearchRequestHandler(
    IProductRepository _productRepository,
    IStringSearchCriteriaWorker _searchWorker) : ISearchRequestHandler<UniversalSearchRequest>, ISearchRequestHandler<ProductSearchRequest>
{
    public Task HandleAsync(SearchRequestHandlerContext<UniversalSearchRequest> context, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken)
    {
        var contextAdapter = new SearchRequestHandlerContext<ProductSearchRequest>()
        {
            Request = ProductSearchRequest.Create(context.Request),
            HandlerConfiguration = context.HandlerConfiguration,
        };

        return HandleAsync(contextAdapter, callback, cancellationToken);
    }

    public async Task HandleAsync(SearchRequestHandlerContext<ProductSearchRequest> context, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            var productsQuery = _searchWorker.SearchItems(products, context.Request.SearchTerm);

            productsQuery = productsQuery.WhereMatchesCriteria(p => p.InStock, context.Request.InStock);
            productsQuery = productsQuery.WhereMatchesCriteria(p => p.InStock, context.Request.InStock);
            productsQuery = productsQuery.WhereMatchesCriteria(p => p.Sold, context.Request.Sold);
            productsQuery = productsQuery.WhereMatchesCriteria(p => p.Created, context.Request.Created);
            productsQuery = productsQuery.WhereMatchesCriteria(p => p.LastChanged, context.Request.LastChanged);

            var searchedProducts = productsQuery.Select(p => new ProductSearchResult(p)).ToList();
            await callback.ReportResultsAsync(context.HandlerConfiguration.HandlerId, searchedProducts);
        }
        catch (Exception ex)
        {
            await callback.ReportFailedAsync(context.HandlerConfiguration.HandlerId, ex);
        }
    }
}
