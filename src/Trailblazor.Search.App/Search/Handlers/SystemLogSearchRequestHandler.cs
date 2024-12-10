using Trailblazor.Search.App.Persistence;
using Trailblazor.Search.Requests;
using Trailblazor.Search.Requests.Criteria.Extensions;
using Trailblazor.Search.Workers;

namespace Trailblazor.Search.App.Search.Handlers;

internal sealed class SystemLogSearchRequestHandler(
    ISystemLogRepository _systemLogRepository,
    ISearchWorker _searchWorker) : ISearchRequestHandler<UniversalSearchRequest>, ISearchRequestHandler<SystemLogSearchRequest>
{
    public Task HandleAsync(SearchRequestHandlerContext<UniversalSearchRequest> context, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken)
    {
        var requestContext = new SearchRequestHandlerContext<SystemLogSearchRequest>()
        {
            Request = SystemLogSearchRequest.Create(context.Request),
            HandlerConfiguration = context.HandlerConfiguration,
        };

        return HandleAsync(requestContext, callback, cancellationToken);
    }

    public async Task HandleAsync(SearchRequestHandlerContext<SystemLogSearchRequest> context, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken)
    {
        try
        {
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
            await callback.ReportResultsAsync(context.HandlerConfiguration.HandlerId, searchedSystemLogs);
        }
        catch (Exception ex)
        {
            await callback.ReportFailedAsync(context.HandlerConfiguration.HandlerId, ex);
        }
    }
}
