namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchRequestThreadConfiguration : ISearchRequestThreadConfiguration
{
    public List<Type> InternalRequestHandlerTypes { get; set; } = [];
    public int Priority { get; set; }
    public IReadOnlyList<Type> RequestHandlerTypes => InternalRequestHandlerTypes;
}
