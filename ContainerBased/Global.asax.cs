using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using ContainerBased.Models;
[assembly: ComVisible(false)]

namespace ContainerBased
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var thisAppAssembly = typeof(MvcApplication).Assembly;
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(thisAppAssembly);
            builder.RegisterControllers(thisAppAssembly);

            // provide a factory method for creating new DBEntities
            builder.Register<DBEntities>(ctx => new DBEntities(ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString));

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        
    }
}
