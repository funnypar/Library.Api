using Business.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using FluentValidation;

namespace Business.Validators
{
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        private readonly IPublisherRepository _publisherRepository;
        public PublisherValidator(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;

            RuleFor(publisher => publisher.Name).NotEmpty();
            RuleFor(publisher => publisher.Books).NotEmpty().WithMessage("The Books can not be found !");
            RuleFor(publihser => publihser.Slug).MustAsync(ValidateSlug)
                .WithMessage("This Publisher is already exists in the database !"); ;
        }
        private async Task<bool> ValidateSlug(Publisher publisher, string slug, CancellationToken token = default)
        {
            var existingPublisher = await _publisherRepository.GetPublisherBySlugAsync(slug , token);
            if (existingPublisher != null)
            {
                return existingPublisher.Id == publisher.Id;
            }
            return existingPublisher == null;
        }
    }
}
