using Business.Dtos.Requests;
using Business.Dtos.Responses;
using DataAccess.Models;
using Library.Api.Mapping;

namespace Business.Mapping
{
    public static class BooksMapping
    {
        public static Book MapToBook(this BooksRequestDto bookRequestDto)
        {
            return new Book()
            {
                Title = bookRequestDto.Title,
                PublisherId = bookRequestDto.PublisherId,
                Description = bookRequestDto.Description,
                IsPublished = bookRequestDto.IsPublished,
                PublishedDate = bookRequestDto.PublishedDate,
                BookTags = new List<BookTag>(),
                Authors = new List<Author>(),
                Publisher = new Publisher() { Name = string.Empty },
            };
        }
        public static BookDto MapToBookDto(this Book book)
        {
            return new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                IsPublished = book.IsPublished,
                Images = book.Images.Select(i => i.Id).ToArray(),
                Tags = book.BookTags.Select(bt => bt.Name).ToList(),
                Authors = book.Authors.Select(a => a.Name).ToList(),
                Publisher = book.Publisher.Name,
                Slug = book.Slug,
            };
        }

        public static BooksResponseDto MapToBooksResponseDto(this List<BookDto> books, RequestBooksOptions options)
        {
            return new BooksResponseDto()
            {
                Total = books.Count,
                Page = options.Page ,
                PageSize = options.PageSize ,
                Items = books
            };
        }
    }
}
