namespace Trailblazor.Search.DependencyInjection;

public interface ISearchRequestOperationConfiguration
{
    public string Key { get; init; }
    public Type PipelineInterfaceType { get; init; }
    public Type PipelineImplementationType { get; init; }
    public Type HandlerInterfaceType { get; init; }
    public IReadOnlyList<ISearchRequestThreadConfiguration> ThreadConfigurations { get; }
}
