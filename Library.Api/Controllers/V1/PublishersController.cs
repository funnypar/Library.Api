using Business.Dtos.Requests;
using Business.Services.Interfaces;
using Library.Api.Auth;
using Library.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1
{
    [ApiController]
    public class PublishersController : Controller
    {
        private readonly IPublisherServices _publisherServices;
        public PublishersController(IPublisherServices publisherServices)
        {
            _publisherServices = publisherServices;
        }
        [AllowAnonymous]
        [HttpGet(ApiEndpoints.V1.Publishers.GetAll)]
        public async Task<IActionResult> GetAllPublisher(CancellationToken token = default)
        {
            try
            {
                var publishers = await _publisherServices.GetAllPublisherAsync(token);
                return Ok(publishers);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet(ApiEndpoints.V1.Publishers.Get)]
        public async Task<IActionResult> GetPublisher([FromRoute] string idOrSlug, CancellationToken token = default)
        {
            try
            {
                var publisher = await _publisherServices.GetPublisherAsync(idOrSlug, token);
                return Ok(publisher);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPost(ApiEndpoints.V1.Publishers.Create)]
        public async Task<IActionResult> CreatePublisher([FromBody] PublishersRequestDto publishersRequestDto, CancellationToken token = default)
        {
            try
            {
                var result = await _publisherServices.CreatePublisherAsync(publishersRequestDto, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("Something Went Wrong !");
                }
                return Ok($"{result} --> has been created !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.V1.Publishers.Update)]
        public async Task<IActionResult> UpdatePublisher(Guid id, [FromBody] PublishersRequestDto publishersRequestDto, CancellationToken token = default)
        {
            try
            {
                var result = await _publisherServices.UpdatePublisherAsync(id, publishersRequestDto, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("The Publisher not Found!");
                }
                return Ok($"{result} --> has been updated !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.V1.Publishers.Delete)]
        public async Task<IActionResult> DeletePublisher(Guid id, CancellationToken token = default)
        {
            try
            {
                var result = await _publisherServices.DeletePublisherAsync(id, token);
                if (result == Guid.Empty)
                {
                    return BadRequest("The publisher not Found!");
                }
                return Ok($"{result} --> has been deleted !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
