using Business.Dtos.Requests;
using Business.Services.Interfaces;
using Library.Api.Auth;
using Library.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpGet(ApiEndpoints.V1.Users.GetAll)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
        {
            try
            {
                var users = await _userServices.GetAllUserAsync(cancellationToken);
                return Ok(users);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpGet(ApiEndpoints.V1.Users.Get)]
        public async Task<IActionResult> GetUser([FromRoute] string idOrSlug, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userServices.GetUserAsync(idOrSlug, cancellationToken);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost(ApiEndpoints.V1.Users.Create)]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDto userRequestDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = await _userServices.CreateUserAsync(userRequestDto, cancellationToken);
                if (userId == Guid.Empty)
                {
                    return BadRequest("Something went wrong when we want to create the user.");
                }
                return Ok($"{userId} --> has been created !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.V1.Users.Update)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserRequestDto userRequestDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = await _userServices.UpdateUserAsync(id, userRequestDto, cancellationToken);
                if (userId == Guid.Empty)
                {
                    return BadRequest("Something went wrong when we want to update the user.");
                }
                return Ok($"{userId} --> has been updated !");

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }
        [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.V1.Users.Delete)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = await _userServices.DeleteUserAsync(id, cancellationToken);
                if (userId == Guid.Empty)
                {
                    return BadRequest("Something went wrong when we want to delete the user.");
                }
                return Ok($"{userId} --> has been deleted !");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }
    }
}
