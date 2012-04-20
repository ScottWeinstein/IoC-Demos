using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using Autofac;
using Moq;
using Xunit;
using System.Security.Principal;

namespace UnitTestingWithIoC
{

    public class TestSetup
    {
        public static IContainer Container { get; private set; }

        static TestSetup()
        {
            var app = new global::ContainerBased.MvcApplication();
            var builder = app.ConfigureAutofac(isTest: true);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            builder.Register<HttpContextBase>(ctx => FakeHttpContext()).SingleInstance();
            Container = builder.Build();
        }
    
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();

            request.Setup(req => req.ApplicationPath).Returns("~/");
            request.Setup(req => req.AppRelativeCurrentExecutionFilePath).Returns("~/");
            request.Setup(req => req.PathInfo).Returns(string.Empty);
            response.Setup(res => res.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns((string virtualPath) => virtualPath);
            user.Setup(usr => usr.Identity).Returns(identity.Object);
            identity.SetupGet(ident => ident.IsAuthenticated).Returns(true);

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);
            context.Setup(ctx => ctx.User).Returns(user.Object);

            return context.Object;
        }

    }
}
