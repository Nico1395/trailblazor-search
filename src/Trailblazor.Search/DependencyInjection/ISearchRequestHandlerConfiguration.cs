namespace Trailblazor.Search.DependencyInjection;

public interface ISearchRequestHandlerConfiguration
{
    public Guid HandlerId { get; }
    public Type HandlerType { get; }
    public int Priority { get; }
}
