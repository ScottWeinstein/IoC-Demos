using System;

namespace NonContainerBased.Models
{
    public class MyModel : IDisposable
    {
        private readonly DBEntities _db; // disposable, so need to implement dispose on the class
        
        public MyModel()
        {
            _db = new DBEntities(); //where is it getting the connection string?
        }

        protected virtual void Dispose(bool disposing) // follow the full dispose pattern
        {
        	_db.Dispose();
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}