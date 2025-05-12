using Business.Dtos.Requests;
using Business.Dtos.Responses;
using DataAccess.Models;

namespace Business.Mapping
{
    public static class PublishersMapping
    {
        public static PublishersResponseDto MapToPublisherResponseDto(this IEnumerable<PublisherDto> publishers,int page, int pageSize)
        {
            return new PublishersResponseDto
            {
                Page = page,
                PageSize = pageSize,
                Total = publishers.Count(),
                Publishers = publishers.ToList(),
            };
        }

        public static PublisherDto MapToPublisherDto(this Publisher publisher)
        {
            return new PublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Slug = publisher.Slug,
                Images = publisher.Images.Select(image => image.Id).ToArray(),
                Books = publisher.Books.Select(book => book.MapToBookDto()).ToList(), 
            };
        }
        public static Publisher MapToPublisher(this PublishersRequestDto publishersRequestDto)
        {
            return new Publisher
            {
                Name = publishersRequestDto.Name,
                Books = new List<Book>(),
            };
        }
    }
}
