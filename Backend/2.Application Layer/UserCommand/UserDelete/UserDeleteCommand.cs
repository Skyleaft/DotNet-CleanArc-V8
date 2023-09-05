using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Ardalis.Result;
using DomainLayer.Common;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Application_Layer.UserCommand.UserDelete
{
    public class UserDeleteCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, Result>
    {
        private readonly IContext _context;
        public UserDeleteCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user is null) return Result.NotFound();

            _context.User.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
