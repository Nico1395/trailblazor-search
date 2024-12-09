namespace Trailblazor.Search.DependencyInjection;

public interface ISearchOperationThreadConfiguration
{
    internal List<Type> InternalRequestHandlerTypes { get; set; }
    public int Priority { get; internal set; }
    public IReadOnlyList<Type> RequestHandlerTypes { get; }
}
