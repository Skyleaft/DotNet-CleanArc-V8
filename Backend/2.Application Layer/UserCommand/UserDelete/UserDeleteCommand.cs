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
using Microsoft.AspNetCore.Hosting;

namespace Application_Layer.UserCommand.UserDelete
{
    public record UserDeleteCommand(int Id) : IRequest<Result>
    {

    }

    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, Result>
    {
        private readonly IContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserDeleteCommandHandler(IContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<Result> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Include(x=>x.UserDetail)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user is null) return Result.NotFound();

            if (!string.IsNullOrEmpty(user.UserDetail.PhotoURL))
            {
                string userPhoto = user.UserDetail.PhotoURL.Replace("/", "\\");
                string pathToDelete = _webHostEnvironment.WebRootPath+userPhoto;
                var checkFile=File.Exists(pathToDelete);
                if (checkFile) { File.Delete(pathToDelete); }
                
            }
            
            _context.User.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
