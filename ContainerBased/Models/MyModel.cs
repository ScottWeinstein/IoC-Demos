using System;
using ContainerBased.DataService;
namespace ContainerBased.Models
{
    public class MyModel
    {
        private readonly IDBEntities _db;
        private readonly IDataService _ds;
        
        public MyModel(IDBEntities db, AdvancedModel am, IDataService ds)
        {
            _db = db;
            _ds = ds;
        }

        public string SaveSomething()
        {
            return _ds.GetData(2);
        }
    }
}