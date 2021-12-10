using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Core.Repositories;
using FA.JustBlog.Core.Core.UnitOfWork;
using FA.JustBlog.Services.Services.Implement;
using System.Web.Http;
using System.Web.Mvc;

namespace FA.JustBlog.WebUI
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();
            //register
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<BlogContext>().AsSelf().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            

            // Scan an assembly for components
            builder.RegisterAssemblyTypes(typeof(PostRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(PostService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}