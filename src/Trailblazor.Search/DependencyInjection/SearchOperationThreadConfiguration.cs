namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationThreadConfiguration : ISearchOperationThreadConfiguration
{
    public List<SearchRequestHandlerConfiguration> InternalHandlerConfigurations { get; } = [];
    public IReadOnlyList<ISearchRequestHandlerConfiguration> HandlerConfigurations => InternalHandlerConfigurations;
}
