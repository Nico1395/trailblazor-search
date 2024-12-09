namespace Trailblazor.Search.Workers;

public interface ISearchWorker
{
    public IQueryable<TItem> SearchItems<TItem>(IEnumerable<TItem> items, ISearchWorkerDescriptor descriptor);
}
