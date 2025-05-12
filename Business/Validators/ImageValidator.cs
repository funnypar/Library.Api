using DataAccess.Models;
using DataAccess.Repositories.Implementations;
using FluentValidation;
using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Business.Validators
{
    public class ImageValidator : AbstractValidator<Image>
    {
        private readonly DataContext _dataContext;

        public ImageValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(image => image)
                .Must(image => image.UserId.HasValue || image.PublisherId.HasValue || image.BookId.HasValue || image.AuthorDetailId.HasValue)
                .WithMessage("At least one of UserId, PublisherId, AuthorDetailId or BookId must have a valid value.");

            RuleFor(image => image.UserId)
                .MustAsync(async (userId, cancellation) =>
                    !userId.HasValue || await _dataContext.Users.AnyAsync(u => u.Id == userId.Value, cancellation))
                .WithMessage("UserId is invalid when provided.");

            RuleFor(image => image.PublisherId)
                .MustAsync(async (publisherId, cancellation) =>
                    !publisherId.HasValue || await _dataContext.Publishers.AnyAsync(p => p.Id == publisherId.Value, cancellation))
                .WithMessage("PublisherId is invalid when provided.");

            RuleFor(image => image.BookId)
                .MustAsync(async (bookId, cancellation) =>
                    !bookId.HasValue || await _dataContext.Books.AnyAsync(b => b.Id == bookId.Value, cancellation))
                .WithMessage("BookId is invalid when provided.");

            RuleFor(image => image.AuthorDetailId)
                .MustAsync(async (authorDetailId, cancellation) =>
                    !authorDetailId.HasValue || await _dataContext.AuthorDetails.AnyAsync(a => a.Id == authorDetailId.Value, cancellation))
                .WithMessage("AuthorDetailId is invalid when provided.");

            RuleFor(image => image.Name)
                .NotEmpty().WithMessage("Image's Name must have value");

            RuleFor(image => image.ContentType)
                .NotEmpty().WithMessage("Image's ContentType must have value");

            RuleFor(image => image.Data)
                .NotEmpty().WithMessage("Image's Data must have value");
        }
    }
}
