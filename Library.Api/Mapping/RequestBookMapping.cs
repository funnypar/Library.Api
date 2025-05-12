using Library.Api.Models.Requests;

namespace Library.Api.Mapping
{
    public static class RequestBookMapping
    {
        public static RequestBooksOptions MapToRequestBooksOptions(this GetAllBooksRequest request)
        {
            return new RequestBooksOptions
            {
                Title = request.Title,
                SortField = request?.SortBy?.Trim('+', '-') ?? "publishedDate",
                SortOrder = request?.SortBy is null ? SortOrder.Unsorted :
                    request.SortBy.StartsWith('-') ? SortOrder.Descending : SortOrder.Ascending,
                Page = request?.Page ?? 1,
                PageSize = request?.PageSize ?? 10
            };
        }
    }
}
