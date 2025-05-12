using Business.Dtos.Requests;
using Business.Dtos.Responses;
using DataAccess.Models;

namespace Business.Mapping
{
    public static class BookTagsMapping
    {
        public static BookTagsResponseDto MapToBookTagsResponseDto(this List<BookTagDto> bookTagsDto, int page, int pageSize)
        {
            return new BookTagsResponseDto
            {
                Page = page,
                PageSize = pageSize,
                Total = bookTagsDto.Count,
                BookTags = bookTagsDto,
            };
        }

        public static BookTagDto MapToBookTagsDto(this BookTag bookTag)
        {
            return new BookTagDto
            {
                Id = bookTag.Id,
                Name = bookTag.Name,
                Slug = bookTag.Slug,
                Books = bookTag.Books.Select(book => book.MapToBookDto()).ToList(),
            };
        }

        public static BookTag MapToBookTag(this BookTagsRequestDto bookTag)
        {
            return new BookTag
            {
                Name = bookTag.Name,
                Books = new List<Book>()
            };
        }
    }
}
