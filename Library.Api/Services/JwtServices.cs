using DataAccess.DataContext;
using Library.Api.Auth;
using Library.Api.Models.Requests;
using Library.Api.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Api.Services
{
    public class JwtServices
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;
        public JwtServices(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public async Task<LoginResponse?> Authenticate(LoginRequest loginRequest)
        {
            if(string.IsNullOrWhiteSpace(loginRequest.UserName) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return null; 
            }

            var userAccount = await _dataContext.Users.FirstOrDefaultAsync(user => 
                user.UserName.ToLower().Trim() == loginRequest.UserName.ToLower().Trim() 
                && user.Password == loginRequest.Password
                );
            if(userAccount == null)
            {
                return null;
            }

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
            var isAdmin = (loginRequest.UserName.Trim().ToLower() == "admin").ToString();
            var isTrustedMember = (loginRequest.UserName.Trim().ToLower() == "string").ToString();
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, loginRequest.UserName),
                    new Claim(AuthConstants.AdminUserClaimName, isAdmin),
                    new Claim(AuthConstants.TrustedMemberClaimName, isTrustedMember)
                }),
                Audience = audience,
                Issuer = issuer,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescripter);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponse
            {
                IsAdmin = isAdmin,
                IsTrustedMember = isTrustedMember,
                AccessToken = accessToken,
                UserName = loginRequest.UserName,
                TokenExp = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
