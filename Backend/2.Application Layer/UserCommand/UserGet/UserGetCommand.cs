using DomainLayer.Extensions;
using Mapster;
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
using DomainLayer.Common.Responses;
using DomainLayer.Common.Requests;
using DomainLayer.Common.Helpers;

namespace Application_Layer.UserCommand.UserGet
{
    public record UserGetCommand : PaginatedRequest,IRequest<PaginatedList<User>>
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public int? RoleID { get; set; }
        public string? Name { get; set; }
        public string? SortBy { get; init; }
        public string? OrderDirection { get; set; }
    }

    public class UserGetCommandHandler : IRequestHandler<UserGetCommand, PaginatedList<User>>
    {
        private readonly DataContext context;

        public UserGetCommandHandler(DataContext context)
        {
			this.context = context;
        }

        public async Task<PaginatedList<User>> Handle(UserGetCommand request, CancellationToken cancellationToken)
        {
            var item = context.User
                .WhereIf(!string.IsNullOrEmpty(request.Username), x => EF.Functions.Like(x.Username, $"%{request.Username}%"))
                .WhereIf(request.Id != null, x => x.Id == request.Id)
                .WhereIf(request.RoleID != null, x => x.RoleId == request.RoleID)
                .ApplyOrderBy(request.SortBy, request.OrderDirection);
            
            return await item.ProjectToType<User>().ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }
    }
}
