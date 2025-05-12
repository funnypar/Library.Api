using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Mapping;
using Business.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using FluentValidation;

namespace Business.Services.Implementations
{

    public class ImageServices : IImageServices
    {
        private readonly IImageRepository _imageRepository;
        private readonly IValidator<Image> _imageValidator;
        public ImageServices(IImageRepository imageRepository , IValidator<Image> imageValidator)
        {
            _imageRepository = imageRepository;
            _imageValidator = imageValidator;
        }
        public async Task<ImageResponseDto> GetAllImagesAsync(CancellationToken token = default)
        {
            try
            {
                var images = await _imageRepository.GetAllImagesAsync(token);
                if (images == null)
                {
                    return new ImageResponseDto();
                }
                var imagesMapped = images.Select(image => image.MapToImageDto()).ToList();
                var result = imagesMapped.MapToImageResponseDto(total: imagesMapped.Count, page: 1, pageSize: 10);
                return result;
            } 
            catch (Exception ex) 
            {
                throw new Exception($"An unexpected error occurred while getting the Images.{ex.Message}", ex);
            }
        }
        public async Task<ImageResponseDto> GetImageAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var image = await _imageRepository.GetImageByIdAsync(id, token);
                if (image == null)
                {
                    return new ImageResponseDto();
                }
                var mappedImage = new List<ImageDto> { image.MapToImageDto() };
                var result = mappedImage.MapToImageResponseDto(total: 1, page: 1, pageSize: 10);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred while getting the image.{ex.Message}", ex);
            }
        }
        public async Task<Guid> CreateImageAsync(ImageRequestDto imageRequestDto, CancellationToken token = default)
        {
            try
            {
                var image = imageRequestDto.MapToImage();

                await _imageValidator.ValidateAndThrowAsync(image, cancellationToken: token);
                var result = await _imageRepository.CreateImageAsync(image);

                if(!result)
                {
                    return Guid.Empty;
                }
                return image.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred while creating the Image. {ex}", ex);
            }
        }
        public async Task<Guid> DeleteImageAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _imageRepository.DeleteImageByIdAsync(id, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred while deleting the Image. {ex}", ex);

            }
        }
        public async Task<Guid> UpdateImageAsync(Guid id, ImageRequestDto imageRequestDto, CancellationToken token = default)
        {
            try
            {
                var image = imageRequestDto.MapToImage();
                await _imageValidator.ValidateAndThrowAsync(image, cancellationToken: token);
                var result = await _imageRepository.UpdateImageAsync(id, image, token);
                if (!result)
                {
                    return Guid.Empty;
                }
                return id;
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"An unexpected error occurred while updating the Image. {ex}", ex);
            }
        }
    }
}
