using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Ardalis.Result;
using DomainLayer.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DomainLayer.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Application_Layer.UserAuth
{
    public record UserAuthCommand : IRequest<Result<UserToken>>
    {
        public string Username { get; init; } = null!;
        public string Password { get; init; } = null!;
    }

    public class UserAuthCommandHandler : IRequestHandler<UserAuthCommand, Result<UserToken>>
    {
        private readonly IContext _context;
        private readonly TokenConfiguration _appSettings;

        public UserAuthCommandHandler(IOptions<TokenConfiguration> appSettings, IContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<Result<UserToken>> Handle(UserAuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Include(s=>s.UserDetail)
                .Include(s=>s.Role)
                .FirstOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password, cancellationToken);
            //pass encrytp
            //if (user == null || !BC.Verify(request.Password, user.Password))
            if (user == null)
            {
                return Result.Invalid(new List<ValidationError> {
                new ValidationError
                {
                    Identifier = $"{nameof(request.Password)}|{nameof(request.Username)}",
                    ErrorMessage = "Username or password is incorrect"
                }
            });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new ClaimsIdentity(new Claim[]
            {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.UserDetail.Email),
            new(ClaimTypes.Role, user.Role.Name)
            });

            var expDate = DateTime.UtcNow.AddHours(8);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = expDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserToken
            {
                Token = tokenHandler.WriteToken(token),
                ExpiredDate = expDate,
                UserId = user.Id,
                User = user
            };
        }
    }
}
