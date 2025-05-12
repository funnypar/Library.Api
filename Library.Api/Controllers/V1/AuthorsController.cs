using Business.Dtos.Requests;
using Business.Services.Interfaces;
using Library.Api.Auth;
using Library.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1
{
    [ApiController]
    public class AuthorsController : Controller
    {
        private readonly IAuthorServices _authorServices;
        public AuthorsController(IAuthorServices authorServices)
        {
            _authorServices = authorServices;
        }
        [AllowAnonymous]
        [HttpGet(ApiEndpoints.V1.Authors.GetAll)]
        public async Task<IActionResult> GetAllAuthors(CancellationToken token)
        {
            try
            {
                var authors = await _authorServices.GetAllAuthorsAsync(token);
                return Ok(authors);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet(ApiEndpoints.V1.Authors.Get)]
        public async Task<IActionResult> GetAuthor([FromRoute] string idOrSlug, CancellationToken token)
        {
            try
            {
                var author = await _authorServices.GetAuthorAsync(idOrSlug, token);
                if (author == null)
                {
                    return BadRequest("No author found !");
                }
                return Ok(author);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPost(ApiEndpoints.V1.Authors.Create)]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorsRequestDto authorsRequestDto, CancellationToken token)
        {
            try
            {
                var result = await _authorServices.CreateAuthorAsync(authorsRequestDto, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("Somethings went wrong!");
                }
                return Ok($"{result} ==> This has been created !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.V1.Authors.Update)]
        public async Task<IActionResult> UpdateAuthorById(Guid id, [FromBody] AuthorsRequestDto authorsRequestDto, CancellationToken token)
        {
            try
            {

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            var result = await _authorServices.UpdateAuthorAsync(id, authorsRequestDto, token);
            if (result == Guid.Empty)
            {
                return BadRequest("Somethings went wrong!");
            }
            return Ok($"{result} ==> This has been updated !");
        }
        [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.V1.Authors.Delete)]
        public async Task<IActionResult> DeleteAuthorById(Guid id, CancellationToken token)
        {
            try
            {
                var result = await _authorServices.DeleteAuthorAsync(id, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("Author not Found!");
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
