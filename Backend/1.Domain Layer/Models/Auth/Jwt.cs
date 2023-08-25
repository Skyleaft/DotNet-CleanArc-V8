using System;

namespace DomainLayer.Models.Auth
{
    public class Jwt
    {
        public string Token { get; init; } = null!;
        public DateTime ExpDate { get; init; }
        public User User { get; init; }
    }
}
