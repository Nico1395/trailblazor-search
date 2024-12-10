namespace Trailblazor.Search.DependencyInjection;

public interface ISearchOperationThreadConfiguration
{
    public IReadOnlyList<ISearchRequestHandlerConfiguration> HandlerConfigurations { get; }
}
