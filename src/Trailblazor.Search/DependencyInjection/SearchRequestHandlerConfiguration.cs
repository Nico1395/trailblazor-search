namespace Trailblazor.Search.DependencyInjection;

internal sealed record SearchRequestHandlerConfiguration : ISearchRequestHandlerConfiguration
{
    public Guid HandlerId { get; } = Guid.NewGuid();
    public required Type HandlerType { get; set; }
    public required int Priority { get; set; }
}
