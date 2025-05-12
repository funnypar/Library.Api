using Business.Dtos.Requests;
using Business.Dtos.Responses;

namespace Business.Mapping
{
    public static class ImageMapping
    {
        public static ImageDto MapToImageDto(this DataAccess.Models.Image image)
        {
            return new ImageDto
            {
                Name = image.Name,
                ContentType = image.ContentType,
                Data = image.Data,
                Publisher = image.PublisherId.HasValue
                             ? image.PublisherId.Value
                             : Guid.Empty,
                Book = image.BookId.HasValue
                             ? image.BookId.Value
                             : Guid.Empty,
                AuthorDetail = image.AuthorDetailId.HasValue
                             ? image.AuthorDetailId.Value
                             : Guid.Empty,
                User = image.UserId ?? Guid.Empty,
            };
        }
        public static ImageResponseDto MapToImageResponseDto(this List<ImageDto> imagesDto, int total, int page, int pageSize)
        {
            return new ImageResponseDto
            {
                Total = total,
                PageSize = pageSize,
                Page = page,
                Images = imagesDto
            };
        }
        public static DataAccess.Models.Image MapToImage(this ImageRequestDto imageRequestDto)
        {
            return new DataAccess.Models.Image
            {
                Name = imageRequestDto.Name,
                ContentType = imageRequestDto.ContentType,
                Data = imageRequestDto.Data,
                PublisherId = imageRequestDto.Publisher,
                BookId = imageRequestDto.Book,
                AuthorDetailId = imageRequestDto.AuthorDetail,
                UserId = imageRequestDto.User
            };
        }
    }
}
