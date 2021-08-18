using Autofac;
using Billdeer.Business.Handlers.EntityExamples.Commands;
using Billdeer.DataAccess.Abstract;
using Billdeer.DataAccess.Concrete.EntityFramework;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        //protected override void Load(ContainerBuilder builder)
        //{
        //    builder.RegisterType<EntityExampleRepository>().As<IEntityExampleRepository>().SingleInstance();

        //    var assembly = System.Reflection.Assembly.GetExecutingAssembly();


        //    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()

        //            .AsClosedTypesOf(typeof(IRequestHandler<,>));


        //}
    }
}
