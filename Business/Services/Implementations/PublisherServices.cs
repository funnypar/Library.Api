using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Mapping;
using Business.Services.Interfaces;
using DataAccess.DataContext;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Implementations
{
    public class PublisherServices : IPublisherServices
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly DataContext _context;   
        private readonly IValidator<Publisher> _validator;
        public PublisherServices(IPublisherRepository publisherRepository, DataContext dataContext, IValidator<Publisher> validator)
        {
            _publisherRepository = publisherRepository;
            _context = dataContext;
            _validator = validator;
        }
        public async Task<PublishersResponseDto> GetAllPublisherAsync(CancellationToken token = default)
        {
            try
            {
                var publishers = await _publisherRepository.GetAllPublisherAsync(token);
                var mappedPublishers = publishers.Select(publisher => publisher.MapToPublisherDto()).ToList();
                var publisherResponse = mappedPublishers.MapToPublisherResponseDto(page: 1, pageSize: 10);
                return publisherResponse;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the publishers.", ex);
            }
        }
        public async Task<PublishersResponseDto> GetPublisherAsync(string idOrSlug, CancellationToken token = default)
        {
            try
            {
                var publisher = Guid.TryParse(idOrSlug, out var id)
                ? await _publisherRepository.GetPublisherByIdAsync(id, token)
                : await _publisherRepository.GetPublisherBySlugAsync(idOrSlug, token);
                if (publisher == null)
                {
                    return new PublishersResponseDto { Page = 1, PageSize = 10, Total = 0, Publishers = [] };
                }
                var mappedPublisher = new List<PublisherDto>() { publisher.MapToPublisherDto() }.MapToPublisherResponseDto(page: 1, pageSize: 10);
                return mappedPublisher;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while getting the publisher.", ex);
            }
        }
        public async Task<Guid> CreatePublisherAsync(PublishersRequestDto publishersRequestDto, CancellationToken token = default)
        {
            try
            {
                var publisher = publishersRequestDto.MapToPublisher();
                var books = _context.Books.Where(book => publishersRequestDto.Books.Contains(book.Id)).ToList();
                publisher.Books = books;
                publisher.SetSlug();

                await _validator.ValidateAndThrowAsync(publisher, cancellationToken: token);
                var result = await _publisherRepository.CreatePublisherAsync(publisher, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return publisher.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the publisher.", ex);
            }
        }
        public async Task<Guid> UpdatePublisherAsync(Guid id, PublishersRequestDto publishersRequestDto, CancellationToken token = default)
        {
            try
            {
                var publisher = await _context.Publishers.FirstOrDefaultAsync(publisher => publisher.Id == id, cancellationToken: token);
                var books = _context.Books.Where(book => publishersRequestDto.Books.Contains(book.Id)).ToList();
                if (publisher == null)
                {
                    return Guid.Empty;
                }
                publisher.Name = publishersRequestDto.Name;
                publisher.Books.Clear();
                publisher.Books = books;
                publisher.SetSlug();

                await _validator.ValidateAndThrowAsync(publisher, cancellationToken: token);
                await _context.SaveChangesAsync(cancellationToken: token);
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while updating the publisher.", ex);
            }
        }
        public async Task<Guid> DeletePublisherAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _publisherRepository.DeletePublisherByIdAsync(id, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return id;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Cannot delete the publisher because it has associated books.",ex);
            }
        }
    }
}
