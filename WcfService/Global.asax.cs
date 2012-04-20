using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;

namespace WcfService
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<DataService>();
            builder.RegisterType<DataDbImplementer>().AsImplementedInterfaces();
            AutofacHostFactory.Container = builder.Build();
        }
    }
}