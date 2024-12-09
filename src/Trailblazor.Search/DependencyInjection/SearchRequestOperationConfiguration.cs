namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchRequestOperationConfiguration : ISearchRequestOperationConfiguration
{
    public List<ISearchRequestThreadConfiguration> InternalThreadConfigurations { get; } = [];
    public required string Key { get; init; }
    public required Type PipelineInterfaceType { get; init; }
    public required Type PipelineImplementationType { get; init; }
    public required Type HandlerInterfaceType { get; init; }
    public IReadOnlyList<ISearchRequestThreadConfiguration> ThreadConfigurations => InternalThreadConfigurations;
}
