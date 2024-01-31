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
using Application_Layer.UserCommand.UserCreate;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DomainLayer.Extensions;
using Microsoft.AspNetCore.Hosting;
using DomainLayer.Common.Helpers;

namespace Application_Layer.UserCommand.UserUpdate
{
    public class UserUpdateRequest : Modifier
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Password must be 8 characters or more"), MinLength(8)]
        public string Password { get; set; }
        public UserDetailRequest? UserDetail { get; set; }
        public int RoleId { get; set; }
    }
    public class UserUpdateCommand : IRequest<Result<User>>
    {
        public int Id { get; set; }
        public UserUpdateRequest User { get; set; }
        public IFormFile? Photo { get; set; }
    }

    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, Result<User>>
    {
        private readonly DataContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserUpdateCommandHandler(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<Result<User>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var origin = await context.User
                .Include(x => x.UserDetail)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (origin == null) return Result.NotFound();

            origin.Password = request.User.Password;
            origin.Active = request.User.Active;
            origin.UpdatedAt = DateTime.Now;
            origin.UpdatedBy = request.User.UpdatedBy;
            origin.RoleId = request.User.RoleId;

            string newImage = "";
            if (request.Photo != null)
            {
                if (!string.IsNullOrEmpty(origin.UserDetail.PhotoURL))
                {
                    var oriImageName = origin.UserDetail.PhotoURL.Split("/");
                    if (request.Photo.FileName != oriImageName.Last())
                    {
                        ImageHandler imageHandler = new ImageHandler();
                        newImage = await imageHandler.ReplaceImageAsync(_webHostEnvironment.WebRootPath, oriImageName.Last(), request.Photo);

                    }
                }
                else
                {
                    ImageHandler imageHandler = new ImageHandler();
                    newImage = await imageHandler.UploadImageAsync(_webHostEnvironment.WebRootPath, request.Photo);
                }
            }

            origin.UserDetail = request.User.UserDetail.Adapt<UserDetail>();
            origin.UserDetail.PhotoURL = newImage;

            context.User.Update(origin);
            await context.SaveChangesAsync(cancellationToken);
            return origin.Adapt<User>();
        }
    }
}
