using System;
using Autofac;
using Autofac.Features.OwnedInstances;

namespace ContainerBased.Models
{
    public class AdvancedModel
    {
        private readonly Func<DBEntities> _dbFactory;
        private readonly Func<Owned<DBEntities>> _ownedFactory;
        private readonly LifetimeManagementModel _moreAdv;

        public AdvancedModel(Func<DBEntities> dbFactory, Func<Owned<DBEntities>> ownedFactory, LifetimeManagementModel moreAdv)
        {
            _dbFactory = dbFactory;
            _ownedFactory = ownedFactory;
            _moreAdv = moreAdv;
            // What do you think the difference is between Lazy<DBEntities> and Func<DBEntities>?
        }

        public void Proccess()
        {
            _dbFactory().ExecuteFunction("sdf");
            using (var dbowner = _ownedFactory())
            {
                dbowner.Value.ExecuteFunction("foo");
            }
        }
    }
}
