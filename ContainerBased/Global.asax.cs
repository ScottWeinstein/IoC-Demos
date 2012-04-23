using System;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.Wcf;
using ContainerBased.Models;
using System.ServiceModel;
using ContainerBased.DataService;
[assembly: ComVisible(false)]

namespace ContainerBased
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = ConfigureAutofac(isTest: false);
            var container = builder.Build();



            // Wire Autofac into ASP.NET MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); 





            // Std MVC stuff
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        public ContainerBuilder ConfigureAutofac(bool isTest)
        {
            var thisAppAssembly = typeof(MvcApplication).Assembly;
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(thisAppAssembly); // All classes are wired up for instance scope
            builder.RegisterControllers(thisAppAssembly); // wire up all controller classes to be HTTPRequestScoped

            // provide a factory method for creating new DBEntities
            builder.Register<IDBEntities>(ctx => new DBEntities(ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString));

            // config WCF service
            builder.Register(c => new ChannelFactory<IDataService>(new BasicHttpBinding(),
                            new EndpointAddress("http://localhost:47758/DataService.svc")))
                            .SingleInstance();

            builder.Register(c => c.Resolve<ChannelFactory<IDataService>>().CreateChannel())
                   .UseWcfSafeRelease();

            return builder;
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
