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

namespace Application_Layer.UserCommand.UserCreate
{
    public class UserCreateCommand : IRequest<Result<User>>
    {
        [Required]
        [StringLength(30, ErrorMessage = "The value cannot exceed 30 characters. And Less than 3"), MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Password must be 8 characters or more"), MinLength(8)]
        public string Password { get; set; }
        public UserDetail? UserDetail { get; set; }
        public int RoleId { get; set; }
    }

    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Result<User>>
    {
        private readonly IContext _context;
        public UserCreateCommandHandler(IContext context)
        {
            _context = context;
        }

        public async Task<Result<User>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var item = request.Adapt<User>();
            item.CreatedAt = DateTime.Now;
            _context.User.Add(item);
            await _context.SaveChangesAsync();
            return item.Adapt<User>();

        }

    }
}
