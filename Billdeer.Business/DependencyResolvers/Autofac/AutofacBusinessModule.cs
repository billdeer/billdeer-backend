using Autofac;
using Autofac.Extras.DynamicProxy;
using Billdeer.Core.Utilities.Interceptors;
using Billdeer.Core.Utilities.Security.JWT;
using Billdeer.DataAccess.Abstract;
using Billdeer.DataAccess.Concrete.EntityFramework;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterType<EntityExampleRepository>().As<IEntityExampleRepository>().SingleInstance();
            builder.RegisterType<LogRepository>().As<ILogRepository>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<AdvertRepository>().As<IAdvertRepository>().SingleInstance();
            builder.RegisterType<OperationClaimRepository>().As<IOperationClaimRepository>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                            {
                                Selector = new AspectInterceptorSelector()
                            }).SingleInstance().InstancePerDependency();
        }
    }
}
