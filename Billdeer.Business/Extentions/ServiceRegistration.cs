using AutoMapper;
using Billdeer.DataAccess.Abstract;
using Billdeer.DataAccess.Concrete.EntityFramework;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Billdeer.Business.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddBusinessRegistration(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();
            services.AddTransient<IEntityExampleRepository, EntityExampleRepository>();
            services.AddAutoMapper(assm);
            services.AddMediatR(assm);
        }
    }
}
