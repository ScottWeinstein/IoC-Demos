using System;
using System.Collections.Generic;
using System.Linq;

namespace WcfService
{
    public class DataService : IDataService
    {
        public DataService(IDataDb db)
        {
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
    }
}
