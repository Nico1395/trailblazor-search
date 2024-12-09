namespace Trailblazor.Search.DependencyInjection;

public interface ISearchRequestThreadConfiguration
{
    internal List<Type> InternalRequestHandlerTypes { get; set; }
    public int Priority { get; internal set; }
    public IReadOnlyList<Type> RequestHandlerTypes { get; }
}
