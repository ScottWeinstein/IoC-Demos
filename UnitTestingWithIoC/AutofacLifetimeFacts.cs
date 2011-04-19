using System;
using Xunit;
using Autofac;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestingWithIoC
{
    public class AutofacLifetimeFacts
    {
        public class DisposableObject : IDisposable
        {
            public bool IsDisposed { get; set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        [Fact]
        public void DisposableObjectIsNotDisposedAtStartup()
        {
            Assert.False(new DisposableObject().IsDisposed);
        }

        [Fact]
        public void DisposableObjectIsDisposedAfterDisposal()
        {
            DisposableObject disp = new DisposableObject();
            disp.Dispose();
            Assert.True(disp.IsDisposed);
        }

        [Fact]
        public void AutofacWillDisposeAllItemsInLifetime()
        {
            List<DisposableObject> list = new List<DisposableObject>();
            using(var scope = TestSetup.Container.BeginLifetimeScope())
            {
                for (int i = 0; i < 100; i++)
                {
                    list.Add(scope.Resolve<DisposableObject>());
                }
                Assert.True(list.All(item => !item.IsDisposed));
            }

            Assert.True(list.All(item => item.IsDisposed));
        }
    }
}
