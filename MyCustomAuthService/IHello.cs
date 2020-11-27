using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyCustomAuthService
{
    [ServiceContract]
    public interface IHello
    {
        [OperationContract]
        string Hello(int value);

        [OperationContract]
        string World(int value);

    }

}
