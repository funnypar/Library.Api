using Business.Helpers;

namespace Business.Dtos.Requests
{
    public class BooksRequestDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonDateTimeConversion))]
        public DateTime? PublishedDate { get; set; }
        public required ICollection<Guid> BookTags { get; set; }
        public required ICollection<Guid> Authors { get; set; }
        public bool IsPublished { get; set; } = true;
        public string[] Images { get; set; } = new string[0];
        public required Guid PublisherId { get; set; }
    }
    
}
