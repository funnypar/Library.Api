using Business.Dtos.Requests;
using Business.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using Library.Api.Auth;
using Library.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1
{
    [ApiController]
    public class BookTagsController : Controller
    {
        private readonly IBookTagServices _bookTagServices;

        public BookTagsController(IBookTagServices bookTagServices)
        {
            _bookTagServices = bookTagServices;
        }
        [AllowAnonymous]
        [HttpGet(ApiEndpoints.V1.BookTags.GetAll)]
        public async Task<IActionResult> GetAllBookTags(CancellationToken token = default)
        {
            try
            {
                var bookTags = await _bookTagServices.GetAllBookTagsAsync(token);
                return Ok(bookTags);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet(ApiEndpoints.V1.BookTags.Get)]
        public async Task<IActionResult> GetBookTag([FromRoute] string idOrSlug, CancellationToken token = default)
        {
            try
            {
                var reslut = await _bookTagServices.GetBookTagAsync(idOrSlug, token);
                return Ok(reslut);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPost(ApiEndpoints.V1.BookTags.Create)]
        public async Task<IActionResult> CreateBookTag([FromBody] BookTagsRequestDto bookTagsRequestDto, CancellationToken token = default)
        {
            try
            {
                var id = await _bookTagServices.CreateBookTagAsync(bookTagsRequestDto, token);
                if (id == Guid.Empty)
                {
                    return BadRequest("Somethings went wrong !");
                }
                return Ok($"{id} --> has been created !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.V1.BookTags.Update)]
        public async Task<IActionResult> UpdateBookTag([FromRoute] Guid id, BookTagsRequestDto bookTagsRequestDto, CancellationToken token = default)
        {
            try
            {
                var result = await _bookTagServices.UpdateBookTagAsync(id, bookTagsRequestDto, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("The BookTag not Found !");
                }
                return Ok($"{result} --> has been updated !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.V1.BookTags.Delete)]
        public async Task<IActionResult> DeleteBookTag([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _bookTagServices.DeleteBookTagAsync(id, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("The BookTag not Found!");
                }
                return Ok($"{result} --> has been deleted!");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
