using Business.Dtos.Requests;
using Business.Dtos.Responses;
using DataAccess.Models;

namespace Business.Mapping
{
    public static class AuthorsMapping
    {

        public static AuthorDto MapToAuthorDto(this Author author)
        {
            return new AuthorDto()
            {
                Id = author.Id,
                Name = author.Name,
                Slug = author.Slug,
                AuthorDetail = new AuthorDetailDto
                {
                    Age = author?.AuthorDetail?.Age,
                    Website = author?.AuthorDetail?.Website,
                    Email = author?.AuthorDetail?.Email,
                    Phone = author?.AuthorDetail?.Phone,
                    Images = author?.AuthorDetail?.Images?.Select(img => img.Id).ToArray()
                },
                Books = author?.Books?.Select(book => book.Title).ToList(),
            };
        }

        public static AuthorsResponseDto MapToAuthorsResponseDto(this List<AuthorDto> authors, int total, int page, int pageSize)
        {
            return new AuthorsResponseDto
            {
                Total = total,
                Page = page,
                PageSize = pageSize,
                Authors = authors
            };
        }

        public static Author MapToAuthor(this AuthorsRequestDto authorsRequest)
        {
            return new Author
            {
                Name = authorsRequest.Name,
                Books = new List<Book>(),
                AuthorDetail = new AuthorDetail
                {
                    Email = authorsRequest.AuthorsDetail?.Email,
                    Age = authorsRequest.AuthorsDetail?.Age,
                    Phone = authorsRequest.AuthorsDetail?.Phone,
                    Website = authorsRequest.AuthorsDetail?.Website,
                }
            };
        }
    }
}
