﻿using Trailblazor.Search.Criteria;

namespace Trailblazor.Search.App.Search;

public sealed class UniversalSearchRequest : ISearchRequest
{
    public StringSearchCriteria SearchTerm { get; set; } = new();
    public SearchCriteria<DateTime> Created { get; } = new();
    public SearchCriteria<DateTime> LastChanged { get; } = new();
}
