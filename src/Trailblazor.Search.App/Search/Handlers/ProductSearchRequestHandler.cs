using Trailblazor.Search.App.Persistence;
using Trailblazor.Search.Requests;
using Trailblazor.Search.Requests.Criteria.Extensions;
using Trailblazor.Search.Workers;

namespace Trailblazor.Search.App.Search.Handlers;

internal sealed class ProductSearchRequestHandler(
    IProductRepository _productRepository,
    ISearchWorker _searchWorker) : ISearchRequestHandler<UniversalSearchRequest>, ISearchRequestHandler<ProductSearchRequest>
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
            var productsQuery = _searchWorker.SearchItems(products, new SearchTermSearchWorkerDescriptor()
            {
                SearchTerm = context.Request.SearchTerm.Value,
                CaseSensitive = context.Request.SearchTerm.CaseSensitive,
                WholeTerm = context.Request.SearchTerm.WholeTerm,
            });

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
