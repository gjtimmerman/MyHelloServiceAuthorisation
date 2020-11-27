using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace HelloClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key when service has started!");
            Console.ReadKey();
            ChannelFactory<MyCustomAuthService.IHello> cf = new ChannelFactory<MyCustomAuthService.IHello>("MyClientConfig");
            MyCustomAuthService.IHello proxy = cf.CreateChannel();
            Console.WriteLine(proxy.Hello(1));
            Console.WriteLine(proxy.World(1));
            ((ICommunicationObject)proxy).Close();
        }
    }
}
