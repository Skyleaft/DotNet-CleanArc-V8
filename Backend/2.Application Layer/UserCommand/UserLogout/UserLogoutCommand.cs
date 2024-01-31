using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ardalis.Result;
using DomainLayer.Models;
using DomainLayer.Common.Helpers;

namespace Application_Layer.UserCommand.UserLogout
{
    public record UserLogoutCommand(int Id) : IRequest<Result>
    {
    }

    public class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommand,Result>
    {
        private readonly DataContext _context;
        private readonly TokenConfiguration _appSettings;
        public UserLogoutCommandHandler(IOptions<TokenConfiguration> appSettings, DataContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<Result> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
        {
            var token = await _context.UserToken.FirstOrDefaultAsync(x=>x.Id == request.Id);
            if (token is null) return Result.NotFound();
            _context.UserToken.Remove(token);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
