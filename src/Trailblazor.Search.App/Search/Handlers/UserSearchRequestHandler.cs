using Trailblazor.Search.App.Persistence;
using Trailblazor.Search.Requests;
using Trailblazor.Search.Requests.Criteria.Extensions;
using Trailblazor.Search.Workers;

namespace Trailblazor.Search.App.Search.Handlers;

internal sealed class UserSearchRequestHandler(
    IUserRepository _userRepository,
    ISearchWorker _searchWorker) : ISearchRequestHandler<UniversalSearchRequest>, ISearchRequestHandler<UserSearchRequest>
{
    public Task HandleAsync(SearchRequestHandlerContext<UniversalSearchRequest> context, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken)
    {
        var requestContext = new SearchRequestHandlerContext<UserSearchRequest>()
        {
            Request = UserSearchRequest.Create(context.Request),
            HandlerConfiguration = context.HandlerConfiguration,
        };

        return HandleAsync(requestContext, callback, cancellationToken);
    }

    public async Task HandleAsync(SearchRequestHandlerContext<UserSearchRequest> context, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);
            var usersQuery = _searchWorker.SearchItems(users, new SearchTermSearchWorkerDescriptor()
            {
                SearchTerm = context.Request.SearchTerm.Value,
                WholeTerm = context.Request.SearchTerm.WholeTerm,
                CaseSensitive = context.Request.SearchTerm.CaseSensitive,
            });

            usersQuery = usersQuery.WhereMatchesCriteria(p => p.FirstName, context.Request.FirstName);
            usersQuery = usersQuery.WhereMatchesCriteria(p => p.LastName, context.Request.LastName);
            usersQuery = usersQuery.WhereMatchesCriteria(p => p.Created, context.Request.Created);
            usersQuery = usersQuery.WhereMatchesCriteria(p => p.LastChanged, context.Request.LastChanged);

            var searchedUsers = usersQuery.Select(p => new UserSearchResult(p)).ToList();
            await callback.ReportResultsAsync(context.HandlerConfiguration.HandlerId, searchedUsers);
        }
        catch (Exception ex)
        {
            await callback.ReportFailedAsync(context.HandlerConfiguration.HandlerId, ex);
        }
    }
}
