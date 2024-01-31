using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;
using DomainLayer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ardalis.Result;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
using DomainLayer.Extensions;
using DomainLayer.Common.Helpers;

namespace Application_Layer.UserCommand.UserCreate
{
    public record UserDetailRequest
    {
        public string? Name { get; set; }
        [StringLength(40, ErrorMessage = "The value cannot exceed 40 characters. ")]
        public string? Email { get; set; }
        [StringLength(20, ErrorMessage = "The value cannot exceed 20 characters. ")]
        public string? Phone { get; set; }
        public string? PhotoURL { get; set; }
    }
    public class UserCreateCommand : IRequest<Result<User>>
    {
        [Required]
        [StringLength(30, ErrorMessage = "The value cannot exceed 30 characters. And Less than 3"), MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Password must be 8 characters or more"), MinLength(8)]
        public string Password { get; set; }
        public UserDetailRequest? UserDetail { get; set; }
        public int RoleId { get; set; }
        public IFormFile? Photo { get;set; }
        public string webHostEnv { get; private set; } = "";
        public void setWebEnv(string webEnv)
        {
            this.webHostEnv = webEnv;
        }
    }

    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Result<User>>
    {
        private readonly DataContext _context;
        public UserCreateCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<User>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            //check duplicate
            var checkDupe = _context.User.FirstOrDefault(x => x.Username == request.Username);
            if (checkDupe != null)
            {
                return Result.Invalid(new List<ValidationError> {
                new ValidationError
                {
                    Identifier = $"{nameof(request.Username)}",
                    ErrorMessage = $"Username {request.Username} is Already Registered"
                }
            });
            }

            var item = request.Adapt<User>();

            //image Handler
            if (request.Photo != null)
            {
                string ext = Path.GetExtension(request.Photo.FileName);
                // Check the allowed extenstions
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return Result.Error(msg);
                }

                ImageHandler imageHandler = new ImageHandler();
                item.UserDetail.PhotoURL = await imageHandler.UploadImageAsync(request.webHostEnv, request.Photo);

            }
            

            item.CreatedAt = DateTime.Now;
            _context.User.Add(item);
            await _context.SaveChangesAsync();
            return item.Adapt<User>();

        }

    }
}
