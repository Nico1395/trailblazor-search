namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationThreadConfiguration : ISearchOperationThreadConfiguration
{
    public List<Type> InternalRequestHandlerTypes { get; set; } = [];
    public int Priority { get; set; }
    public IReadOnlyList<Type> RequestHandlerTypes => InternalRequestHandlerTypes;
}
