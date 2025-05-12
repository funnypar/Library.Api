using DataAccess.Models;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using FluentValidation;

namespace Business.Validators
{
    public class BookTagValidator : AbstractValidator<BookTag>
    {
        private readonly IBookTagRepository _bookTagRepository;

        public BookTagValidator(IBookTagRepository bookTagRepository)
        {
            _bookTagRepository = bookTagRepository;
            RuleFor(bookTag => bookTag.Name).NotEmpty();
            RuleFor(bookTag => bookTag.Books).NotEmpty().WithMessage("The Books not found!");
            RuleFor(bookTag => bookTag.Slug).MustAsync(ValidateSlug)
               .WithMessage("This BookTag is already exists in the database !");
        }
        private async Task<bool> ValidateSlug(BookTag bookTag, string slug, CancellationToken token = default)
        {
            var existingBookTag = await _bookTagRepository.GetBookTagBySlugAsync(slug);
            if (existingBookTag != null)
            {
                return existingBookTag.Id == bookTag.Id;
            }
            return existingBookTag == null;
        }
    }
}
