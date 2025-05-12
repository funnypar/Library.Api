
using Business.Dtos.Requests;
using Business.Services.Interfaces;
using Library.Api.Auth;
using Library.Api.Constants;
using Library.Api.Mapping;
using Library.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Library.Api.Controllers.V1
{

    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookServices _bookServices;
        private readonly IOutputCacheStore _outputCacheStore;
        public BooksController(IBookServices bookServices, IOutputCacheStore outputCacheStore)
        {
            _bookServices = bookServices;
            _outputCacheStore = outputCacheStore;   
        }

        [HttpGet(ApiEndpoints.V1.Books.GetAll)]
        [AllowAnonymous]
        [ResponseCache(Duration = 30, VaryByHeader = "Accept, Accept-Encoding", Location = ResponseCacheLocation.Any)]
        [OutputCache(PolicyName = "BooksCache")]
        public async Task<IActionResult> GetAllBooks([FromQuery] GetAllBooksRequest request, CancellationToken token)
        {
            try
            {
                var options = request.MapToRequestBooksOptions();
                var books = await _bookServices.GetAllBooksAsync(options, token);
                return Ok(books);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet(ApiEndpoints.V1.Books.Get)]
        [ResponseCache(Duration = 30, VaryByHeader = "Accept, Accept-Encoding", Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetBook([FromQuery] GetAllBooksRequest request, [FromRoute] string idOrSlug, CancellationToken token)
        {
            try
            {
                var options = request.MapToRequestBooksOptions();
                var book = await _bookServices.GetBookAsync(options, idOrSlug, token);
                if (book == null)
                {
                    return BadRequest("Book has not found !");
                }
                return Ok(book);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPost(ApiEndpoints.V1.Books.Create)]
        public async Task<IActionResult> CreateBook([FromBody] BooksRequestDto booksRequestDto, CancellationToken token)
        {
            try
            {
                var result = await _bookServices.CreateBookAsync(booksRequestDto, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("Something went wrong !");
                }
                await _outputCacheStore.EvictByTagAsync("Books",token);
                return Ok($"{result} --> has been created!");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.V1.Books.Update)]
        public async Task<IActionResult> UpdateBookById([FromRoute] Guid id, [FromBody] BooksRequestDto booksRequestDto, CancellationToken token)
        {
            try
            {
                var result = await _bookServices.UpdateBookByIdAsync(id, booksRequestDto, token);
                if (result.Equals(null))
                {
                    return BadRequest("The book has not found !");
                }
                await _outputCacheStore.EvictByTagAsync("Books", token);
                return Ok($"{result} --> The Book has been updated !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.V1.Books.Delete)]
        public async Task<IActionResult> DeleteBookById([FromRoute] Guid id, CancellationToken token)
        {
            try
            {
                var result = await _bookServices.DeleteBookByIdAsync(id, token);
                if (result.Equals(Empty))
                {
                    return BadRequest("Book has not found !");
                }
                await _outputCacheStore.EvictByTagAsync("Books", token);
                return Ok($"{result} -->  has been deleted !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
