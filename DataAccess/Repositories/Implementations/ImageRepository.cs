using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext.DataContext _dataContext;
        public ImageRepository(DataContext.DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Image>> GetAllImagesAsync(CancellationToken token = default)
        {
            try
            {
                return await _dataContext.Images.ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"There is a problem in getting Images.{ex.Message}", ex);
            }
        }
        public async Task<Image?> GetImageByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                return await _dataContext.Images.FirstOrDefaultAsync(image => image.Id == id, cancellationToken: token);
            }
            catch (Exception ex)
            {
                throw new Exception($"There is a problem in getting Image.{ex.Message}", ex);
            }
        }
        public async Task<bool> DeleteImageByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _dataContext.Images.Where(image => image.Id == id).ExecuteDeleteAsync(cancellationToken: token);
                if (result == 0)
                {
                    return false;
                }
                await _dataContext.SaveChangesAsync(cancellationToken: token);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"There is a problem in deleting Image.{ex.Message}", ex);
            }
            
        }
        public async Task<bool> CreateImageAsync(Image image, CancellationToken token = default)
        {
            try
            {
                var result = await _dataContext.Images.AddAsync(image, cancellationToken: token);

                if (result == null)
                {
                    return false;
                }
                await _dataContext.SaveChangesAsync(cancellationToken: token);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"There is a problem in creating Image.{ex.Message}", ex);
            }
        }
        public async Task<bool> UpdateImageAsync(Guid id, Image image, CancellationToken token = default)
        {
            try
            {
                var foundImage = await _dataContext.Images.FirstOrDefaultAsync(image => image.Id == id);
                if (foundImage == null)
                {
                    return false;
                }

                foundImage.AuthorDetailId = image.AuthorDetailId;
                foundImage.PublisherId = image.PublisherId;
                foundImage.UserId = image.UserId;
                foundImage.BookId = image.BookId;
                foundImage.Data = image.Data;
                foundImage.Name = image.Name;
                foundImage.ContentType = image.ContentType;

                await _dataContext.SaveChangesAsync(token);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"There is a problem in updating Image.{ex.Message}", ex);
            }
        }
    }
}
