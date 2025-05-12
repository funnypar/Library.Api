using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Mapping;
using Business.Services.Interfaces;
using Business.Validators;
using DataAccess.DataContext;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Implementations
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IAuthorRepository _AuthorRepository;
        private readonly DataContext _dataContext;
        private readonly IValidator<Author> _authorValidator;
        public AuthorServices(IAuthorRepository authorRepository, DataContext dataContext, IValidator<Author> validator)
        {
            _AuthorRepository = authorRepository;
            _dataContext = dataContext;
            _authorValidator = validator;
        }
        public async Task<AuthorsResponseDto> GetAllAuthorsAsync(CancellationToken token = default)
        {
            try
            {
                var authors = await _AuthorRepository.GetAllAuthorsAsync(token);
                if (authors == null)
                {
                    return new AuthorsResponseDto();
                }
                var authorsMapped = authors.Select(author => author.MapToAuthorDto()).ToList();
                var result = authorsMapped.MapToAuthorsResponseDto(total: authorsMapped.Count, page: 1, pageSize: 10);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the authors.", ex);
            }
        }
        public async Task<AuthorsResponseDto> GetAuthorAsync(string idOrSlug, CancellationToken token = default)
        {
            try
            {
                var author = Guid.TryParse(idOrSlug, out var id) ? await _AuthorRepository.GetAuthorByIdAsync(id, token)
                                : await _AuthorRepository.GetAuthorBySlugAsync(idOrSlug, token);
                if (author == null)
                {
                    return new AuthorsResponseDto();
                }
                var mappedAuthor = new List<AuthorDto> { author.MapToAuthorDto() };
                var result = mappedAuthor.MapToAuthorsResponseDto(page: 1, pageSize: 10, total: 1);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the author.", ex);
            }
        }
        public async Task<Guid> CreateAuthorAsync(AuthorsRequestDto authorRequestDto, CancellationToken token = default)
        {
            try
            {
                var books = _dataContext.Books
                        .Where(book => authorRequestDto.Books.Contains(book.Id))
                        .ToList();
                var imageGuids = authorRequestDto.AuthorsDetail?.Images?.ToList(); 
                var images = await _dataContext.Images
                    .Where(bookImage => imageGuids.Contains(bookImage.Id))
                    .ToArrayAsync(cancellationToken: token);


                var author = authorRequestDto.MapToAuthor();
                author.Books = books;
                author.SetSlug();

                await _authorValidator.ValidateAndThrowAsync(author, cancellationToken: token);
                var createdAuthor = await _AuthorRepository.CreateAuthorAsync(author, token);
                if (!createdAuthor)
                {
                    return Guid.Empty;
                }
                return author.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the author.", ex);
            }
            
        }
        public async Task<Guid> UpdateAuthorAsync (Guid id,AuthorsRequestDto authorRequestDto, CancellationToken token = default)
        {
            try
            {
                var author = await _dataContext.Authors
                .Include(author => author.AuthorDetail)
                .ThenInclude(authorDetail => authorDetail.Images)
                .Include(author => author.Books)
                .FirstOrDefaultAsync(author => author.Id == id, token);

                if (author == null)
                {
                    return Guid.Empty;
                }
                var books = _dataContext.Books
                            .Where(book => authorRequestDto.Books.Contains(book.Id))
                            .ToList();
                var imageGuids = authorRequestDto.AuthorsDetail?.Images?.ToList();
                var images = await _dataContext.Images
                    .Where(bookImage => imageGuids.Contains(bookImage.Id))
                    .ToArrayAsync(cancellationToken: token);

                if (author.AuthorDetail != null)
                {
                    author.AuthorDetail.Age = authorRequestDto.AuthorsDetail.Age;
                    author.AuthorDetail.Email = authorRequestDto.AuthorsDetail.Email;
                    author.AuthorDetail.Phone = authorRequestDto.AuthorsDetail.Phone;
                    author.AuthorDetail.Website = authorRequestDto.AuthorsDetail.Website;
                    author.AuthorDetail.Images = images;
                }
                author.Name = authorRequestDto.Name;
                author.Books.Clear();
                author.Books = books;
                author.SetSlug();

                await _authorValidator.ValidateAndThrowAsync(author, cancellationToken: token);
                await _dataContext.SaveChangesAsync(token);
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while updating the author.", ex);
            }
        }
        public async Task<Guid> DeleteAuthorAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _AuthorRepository.DeleteAuthorByIdAsync(id, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while deleting the author.", ex);
            }
        }
    }
}
