using DataAccess.DataContext;
using DataAccess.Repositories.Interfaces;
using FluentValidation;

namespace Business.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        private readonly IBookRepository _bookRepository;
        public BookValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            RuleFor(book => book.Authors).NotEmpty();
            RuleFor(book => book.Publisher).NotEmpty();
            RuleFor(book => book.PublishedDate).LessThan(DateTime.UtcNow);
            RuleFor(book => book.Slug).MustAsync(ValidateSlug)
                .WithMessage("This Book is already exists in the database !");
        }
        private async Task<bool> ValidateSlug(Book book, string slug, CancellationToken token = default)
        {

            var existingBook = await _bookRepository.GetBookBySlugAsync(slug);
            if (existingBook != null)
            {
                return existingBook.Id == book.Id;
            }
            return existingBook == null;
        }
    }
}
