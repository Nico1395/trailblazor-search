namespace Trailblazor.Search;

/// <inheritdoc/>
public sealed class ConcurrentSearchResponse(List<Guid> handlerIds) : IConcurrentSearchResponse
{
    private readonly Lock _lock = new();
    private readonly List<ISearchResult> _results = [];

    /// <inheritdoc/>
    public IReadOnlyList<ISearchResult> Results
    {
        get
        {
            lock (_lock)
                return _results;
        }
    }

    /// <inheritdoc/>
    public IConcurrentSearchResponseState State { get; } = new ConcurrentSearchResponseState(handlerIds);

    internal void AddResults(IReadOnlyList<ISearchResult> results)
    {
        lock (_lock)
        {
            foreach (var result in results)
            {
                _results.Remove(result);
                _results.Add(result);
            }
        }
    }
}
