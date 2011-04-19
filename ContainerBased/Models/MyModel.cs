using System;
namespace ContainerBased.Models
{
    public class MyModel
    {
        private readonly DBEntities _db;
        
        public MyModel(DBEntities db, AdvancedModel am)
        {
            _db = db;
        }
    }
}