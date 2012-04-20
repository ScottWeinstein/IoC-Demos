using System;
using Autofac;

namespace ContainerBased.Models
{
    public class LifetimeManagementModel
    {
        private readonly ILifetimeScope _scope;
        public LifetimeManagementModel(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Proccess()
        {
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Tasks.Task.Factory.StartNew( () =>
                {
                    using (var localscope = _scope.BeginLifetimeScope())
                    {
                        var db = localscope.Resolve<DBEntities>(); // will get cleaned up at the end of the using block
                    }
                });
            }
        }

    }
}
