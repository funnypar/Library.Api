namespace Library.Api.Models.Responses
{
    public class LoginResponse
    {
        public required string UserName { get; set; }
        public required string AccessToken { get; set; }
        public required int TokenExp { get; set; }
        public required string IsAdmin { get; set; }
        public required string IsTrustedMember { get; set; }
    }
}
