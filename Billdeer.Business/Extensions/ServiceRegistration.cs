using Autofac;
using Billdeer.Business.Handlers.EntityExamples.Commands;
using Billdeer.Business.Handlers.EntityExamples.Queries;
using Billdeer.Core.Utilities.Results;
using Billdeer.DataAccess.Abstract;
using Billdeer.DataAccess.Concrete.EntityFramework;
using Billdeer.Entities.DTOs.EntityExampleDtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Billdeer.Business.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddBusinessRegistration(this IServiceCollection services)
        {
            //var assm = Assembly.GetExecutingAssembly();
            //services.AddAutoMapper(assm);
            //services.AddMediatR(assm);
        }
        //public static ContainerBuilder AddGenericHandlers(this ContainerBuilder builder)
        //{
        //    builder.RegisterGeneric(typeof(CreateEntityExampleCommandHandler)).AsImplementedInterfaces()
        //    return builder;
        //}
    }
}
