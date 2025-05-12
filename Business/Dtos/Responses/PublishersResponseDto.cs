namespace Business.Dtos.Responses
{
    public class PublishersResponseDto
    {
        public int Total { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<PublisherDto> Publishers { get; set; } = new List<PublisherDto>();
    }
}

