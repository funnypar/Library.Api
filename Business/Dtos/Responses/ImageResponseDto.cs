namespace Business.Dtos.Responses
{
    public class ImageResponseDto
    {
        public int Total { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<ImageDto> Images { get; set; } = new List<ImageDto>();
      
    }
}
