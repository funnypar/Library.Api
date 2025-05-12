namespace Business.Dtos.Requests
{
    public class PublishersRequestDto
    {
        public required string Name { get; set; }
        public required ICollection<Guid> Books { get; set; }
    }
}
