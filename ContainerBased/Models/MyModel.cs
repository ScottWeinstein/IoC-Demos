using System;
using ContainerBased.DataService;
namespace ContainerBased.Models
{
    public class MyModel
    {
        private readonly DBEntities _db;
        private readonly IDataService _ds;
        
        public MyModel(DBEntities db, AdvancedModel am, IDataService ds)
        {
            _db = db;
            _ds = ds;

            var res = _ds.GetData(1);
        }
    }
}