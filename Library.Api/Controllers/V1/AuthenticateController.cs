using Library.Api.Constants;
using Library.Api.Models.Requests;
using Library.Api.Models.Responses;
using Library.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1
{
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly JwtServices _jwtServices;
        public AuthenticateController(JwtServices jwtServices)
        {
            _jwtServices = jwtServices;
        }

        [AllowAnonymous]
        [HttpPost(ApiEndpoints.V1.Authenticate.Login)]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var result = await _jwtServices.Authenticate(loginRequest);
            if (result == null)
            {
                return Unauthorized();
            }
            return result;
        }
    }
}
