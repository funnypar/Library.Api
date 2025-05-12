namespace Library.Api.Models.Requests
{
    public class GetAllBooksRequest : PagedRequest
    {
        public string? Title { get; init; }
        public string? SortBy { get; init; }

    }
}
