using Business.Dtos.Requests;
using Business.Services.Implementations;
using Business.Services.Interfaces;
using Library.Api.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1
{
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageServices _imageServices;
        public ImagesController(IImageServices imageServices)
        {
            _imageServices = imageServices;
        }

        [HttpGet(ApiEndpoints.V1.Image.GetAll)]
        public async Task<IActionResult> GetAllImages(CancellationToken token = default)
        {
            try
            {
                var images = await _imageServices.GetAllImagesAsync(token);
                return Ok(images);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet(ApiEndpoints.V1.Image.Get)]
        public async Task<IActionResult> GetImage([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                var image = await _imageServices.GetImageAsync(id, token);
                return Ok(image);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost(ApiEndpoints.V1.Image.Create)]
        public async Task<IActionResult> CreateImage([FromBody] ImageRequestDto imageRequestDto, CancellationToken token = default)
        {
            try
            {
                var result = await _imageServices.CreateImageAsync(imageRequestDto, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("Something went wrong !");
                }
                return Ok($"{result} --> has been created!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut(ApiEndpoints.V1.Image.Update)]
        public async Task<IActionResult> UpdateImage([FromRoute] Guid id, [FromBody] ImageRequestDto imageRequestDto, CancellationToken token = default)
        {
            try
            {
                var result = await _imageServices.UpdateImageAsync(id, imageRequestDto, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("The image has not found !");
                }
                return Ok($"{result} --> The image has been updated !");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete(ApiEndpoints.V1.Image.Delete)]
        public async Task<IActionResult> DeleteImage([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _imageServices.DeleteImageAsync(id, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("Image not Found!");
                }
                return Ok($"{result} -->  has been deleted !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
