namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationThreadConfiguration : ISearchOperationThreadConfiguration
{
    internal List<SearchRequestHandlerConfiguration> InternalHandlerConfigurations { get; } = [];

    public required string Key { get; init; }
    public IReadOnlyList<ISearchRequestHandlerConfiguration> HandlerConfigurations => InternalHandlerConfigurations;
}
