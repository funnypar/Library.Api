﻿namespace Library.Api.Models.Requests
{
    public class PagedRequest
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
