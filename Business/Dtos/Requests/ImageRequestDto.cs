using DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace Business.Dtos.Requests
{
    public class ImageRequestDto
    {
        public required string Name { get; set; }
        public required string ContentType { get; set; }
        public required string Data { get; set; }
        public Guid? Book { get; set; }
        public Guid? Publisher { get; set; }
        public Guid? AuthorDetail { get; set; }
        public Guid? User { get; set; }
    }
}
