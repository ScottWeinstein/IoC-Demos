using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        string GetData(int value);
    }
}
