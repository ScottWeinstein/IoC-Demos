using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using Autofac;
using ContainerBased.DataService;
using ContainerBased.Models;
using Moq;
using Xunit;

namespace UnitTestingWithIoC
{
    public class TestModel
    {
        public TestModel()
        {
        }

        [Fact]
        public void ModelSave_Calls_SaveService()
        {
            var m = TestSetup.Container.Resolve<Mock<IDataService>>();
            m.Setup(ids => ids.GetData(2)).Returns("hello 2");

            var mm = TestSetup.Container.Resolve<MyModel>();
            mm.SaveSomething();
            Assert.NotNull(mm.SaveSomething());
        }
    }

    public class TestSetup
    {
        public static IContainer Container { get; private set; }

        static TestSetup()
        {
            var app = new global::ContainerBased.MvcApplication();
            var builder = app.ConfigureAutofac(isTest: true);

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            builder.Register<IDBEntities>(ctx => new Mock<IDBEntities>().Object);

            var mids = new Mock<IDataService>();
            builder.RegisterInstance(mids);
            builder.Register<IDataService>(ctx => mids.Object);

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
