using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfClient
{
    class Program : IDisposable
    {
        //static  ServiceHostBase asd = new ServiceHostBase() ;
        static InstanceContext context = new InstanceContext(new AsansörCallback());
        static Wcf.AsansorClient server;
        static UserServiceReference.UserServiceClient User;
        static void Main(string[] args)
        {



            NetNamedPipeBinding  binding = new NetNamedPipeBinding();


            EndpointAddress address = new EndpointAddress("net.pipe://localhost/asansor");
            server = new Wcf.AsansorClient(context,binding,address);
            server.ClientReg();
            User = new UserServiceReference.UserServiceClient(binding,new EndpointAddress("net.pipe://localhost/user"));
            var asdg = User.Listele();
            Console.ReadLine();
        }

        public void Dispose()
        {
            server.Close();
        }
    }
}
