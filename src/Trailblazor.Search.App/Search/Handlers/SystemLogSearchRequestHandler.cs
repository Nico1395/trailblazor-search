using Trailblazor.Search.App.Persistence;
using Trailblazor.Search.Extensions;
using Trailblazor.Search.Workers;

namespace Trailblazor.Search.App.Search.Handlers;

internal sealed class SystemLogSearchRequestHandler(
    ISystemLogRepository _systemLogRepository,
    ISearchWorker _searchWorker) : ISearchRequestHandler<UniversalSearchRequest>, ISearchRequestHandler<SystemLogSearchRequest>
{
    public Task HandleAsync(SearchRequestHandlerContext<UniversalSearchRequest> context, IConcurrentSearchRequestCallback callback, CancellationToken cancellationToken)
    {
        var requestContext = new SearchRequestHandlerContext<SystemLogSearchRequest>()
        {
            HandlerId = context.HandlerId,
            Request = SystemLogSearchRequest.Create(context.Request),
        };

        return HandleAsync(requestContext, callback, cancellationToken);
    }

    public async Task HandleAsync(SearchRequestHandlerContext<SystemLogSearchRequest> context, IConcurrentSearchRequestCallback callback, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(999);

            var systemLogs = await _systemLogRepository.GetAllAsync(cancellationToken);
            var systemLogsQuery = _searchWorker.SearchItems(systemLogs, new SearchTermSearchWorkerDescriptor()
            {
                SearchTerm = context.Request.SearchTerm.Value,
                WholeTerm = context.Request.SearchTerm.WholeTerm,
                CaseSensitive = context.Request.SearchTerm.CaseSensitive,
            });

            systemLogsQuery = systemLogsQuery.WhereMatchesCriteria(p => p.Message, context.Request.Message);
            systemLogsQuery = systemLogsQuery.WhereMatchesCriteria(p => p.Created, context.Request.Created);

            var searchedSystemLogs = systemLogsQuery.Select(p => new SystemLogSearchResult(p)).ToList();
            await callback.ReportResultsAsync(context.HandlerId, searchedSystemLogs);
        }
        catch (Exception ex)
        {
            await callback.ReportFailedAsync(context.HandlerId, ex);
        }
    }
}
