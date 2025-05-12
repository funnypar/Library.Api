using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Library.Api.Mapping;

namespace Business.Services.Interfaces
{
    public interface IBookServices
    {
        Task<BooksResponseDto> GetAllBooksAsync(RequestBooksOptions options,CancellationToken token = default);
        Task<BooksResponseDto> GetBookAsync(RequestBooksOptions options , string idOrSlug, CancellationToken token = default);
        Task<Guid> DeleteBookByIdAsync(Guid id, CancellationToken token = default);
        Task<Guid> CreateBookAsync(BooksRequestDto bookRequestDto, CancellationToken token = default);
        Task<Guid> UpdateBookByIdAsync(Guid id, BooksRequestDto bookRequestDto, CancellationToken token = default);
    }
}
