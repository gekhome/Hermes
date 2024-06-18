using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Hermes.DAL;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace Hermes
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<HermesDBEntities>().AsSelf();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service") && t.Namespace.Contains("Services"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
                
                // This breaks the services and causes exceptions
                //.WithParameter("entities", new HermesDBEntities());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}