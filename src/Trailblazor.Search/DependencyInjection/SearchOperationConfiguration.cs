namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationConfiguration : ISearchOperationConfiguration
{
    public List<ISearchOperationThreadConfiguration> InternalThreadConfigurations { get; } = [];
    public required string Key { get; init; }
    public required Type PipelineInterfaceType { get; init; }
    public required Type PipelineImplementationType { get; set; }
    public required Type HandlerInterfaceType { get; init; }
    public Type? CallbackImplementationType { get; set; }
    public IReadOnlyList<ISearchOperationThreadConfiguration> ThreadConfigurations => InternalThreadConfigurations;
}
