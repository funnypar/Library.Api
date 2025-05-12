using Azure;

namespace Business.Dtos.Responses
{
    public class BookTagsResponseDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public required List<BookTagDto> BookTags { get; set; }
    }
}
