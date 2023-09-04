using DomainLayer.Common.Behaviors;
using DomainLayer.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application_Layer
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddMediatR((config) =>
            //{
            //    config.RegisterServicesFromAssemblyContaining(typeof(IAssemblyMarker));
            //    config.AddOpenBehavior(typeof(ValidationResultPipelineBehavior<,>));
            //});
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
