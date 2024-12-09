namespace Trailblazor.Search.DependencyInjection;

public interface ISearchOperationConfiguration
{
    public string Key { get; init; }
    public Type PipelineInterfaceType { get; init; }
    public Type PipelineImplementationType { get; init; }
    public Type HandlerInterfaceType { get; init; }
    public IReadOnlyList<ISearchOperationThreadConfiguration> ThreadConfigurations { get; }
}
