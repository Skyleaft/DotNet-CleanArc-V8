using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DomainLayer.Common;
using DomainLayer.Models;
using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace Application_Layer.UserCommand.UserUpdate
{
    public class UserUpdateCommand : IRequest<Result<User>>
    {
        public int Id { get; set; }
        public string UpdatedBy { get; set; }
        public User User { get; set; }
        public int RoleId { get;set; }
    }

    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand,Result<User>>
    {
        private readonly IContext context;

        public UserUpdateCommandHandler(IContext context)
        {
			this.context = context;
        }

        public async Task<Result<User>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var origin = await context.User
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (origin == null) return Result.NotFound();

            origin.Password = request.User.Password;
            origin.Active = request.User.Active;
            origin.UpdatedAt = DateTime.Now;
            origin.UpdatedBy = request.UpdatedBy;
            origin.RoleId = request.RoleId;
            origin.UserDetail = request.User.UserDetail;
            context.User.Update(origin);
            await context.SaveChangesAsync(cancellationToken);
            return origin.Adapt<User>();
        }
    }
}
