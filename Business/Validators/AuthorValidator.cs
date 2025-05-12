using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using FluentValidation;

namespace Business.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorValidator(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            RuleFor(author => author.Name).NotEmpty();
            RuleFor(author => author.AuthorDetail).NotNull().NotEmpty();
            RuleFor(author => author.AuthorDetail.Age).LessThanOrEqualTo(100).GreaterThanOrEqualTo(10).NotEmpty()
                .WithMessage("Author must have Age and that should be between 10 - 100").When(author => author.AuthorDetail is not null);
            RuleFor(author => author.Books).NotEmpty();
            RuleFor(author => author.Slug).MustAsync(ValidateSlug)
                .WithMessage("This Author is already exists in the database !");
        }
        private async Task<bool> ValidateSlug(Author author, string slug, CancellationToken token = default)
        {
            var existingAuthor = await _authorRepository.GetAuthorBySlugAsync(slug);
            if (existingAuthor != null)
            {
                return existingAuthor.Id == author.Id;
            }
            return existingAuthor == null;
        }
    }
}
