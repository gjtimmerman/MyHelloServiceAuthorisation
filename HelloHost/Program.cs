using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace HelloHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost myHost = new ServiceHost(typeof(MyCustomAuthService.HelloImpl));
            myHost.Open();
            Console.WriteLine("Service has started. Press ENTER to shutdown");
            Console.ReadLine();
            myHost.Close();
        }
    }
}
