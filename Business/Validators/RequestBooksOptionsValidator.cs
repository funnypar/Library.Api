using FluentValidation;
using Library.Api.Mapping;

namespace Business.Validators
{
    public class RequestBooksOptionsValidator : AbstractValidator<RequestBooksOptions>
    {
        private static readonly string[] AcceptableSortFields =
        { 
            "title", "publishedDate"
        };
        public RequestBooksOptionsValidator()
        {
            RuleFor(x => x.SortField).Must(x => x is null || AcceptableSortFields.Contains(x, StringComparer.OrdinalIgnoreCase))
                .WithMessage("You can only sort by 'title' or 'publishedDate' ");

            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Please enter page greater than 0 ");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 25).WithMessage("Please enter pageSize between 1 and 25 ");
        }
    }
}
