
namespace DomainLayer.Models.Auth
{
    public class AuthenticateRequest
    {
        public string Username { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
