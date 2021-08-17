using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Billdeer.Business.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddBusinessRegistration(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assm);
            services.AddMediatR(assm);
        }
    }
}
