using DomainLayer.Common;
using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;
using DomainLayer.Extensions;
using Microsoft.AspNetCore.Http;
using Mapster;
using Ardalis.Result;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Common.Helpers;
using System.Data;

namespace Application_Layer.ModuleCommand.TestGetUnmapped
{
    public class TestGetUnmappedCommand : IRequest<Role>
    {
    }

    public class TestGetUnmappedCommandHandler : IRequestHandler<TestGetUnmappedCommand, Role>
    {
        private readonly DataContext context;

        public TestGetUnmappedCommandHandler(DataContext context)
        {
			this.context = context;
        }

        public async Task<Role> Handle(TestGetUnmappedCommand request, CancellationToken cancellationToken)
        {
            var test = await context.Database.SqlQuery<Role>($"Select * from Role").FirstOrDefaultAsync();
            Console.WriteLine(test);
            return test;
        }
    }
}
