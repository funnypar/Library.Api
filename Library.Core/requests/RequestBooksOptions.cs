using Library.Core.Attributes;

namespace Library.Api.Mapping
{
    public class RequestBooksOptions
    {
        public string? Title { get; set; }
        [CapitalizeFirst]
        public string? SortField { get; set; }
        public SortOrder? SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public enum SortOrder
    {
        Unsorted,
        Ascending,
        Descending,
    }
}
