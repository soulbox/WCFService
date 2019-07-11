using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Topshelf;

namespace WcfServiceHost
{
    using Extensions;
    class Program
    {
     
        static void Main(string[] args)
        {
           

            var exitCode = HostFactory.Run(x =>
            {
                x.Service<List<ServiceHost>>(s =>
                {
                    s.ConstructUsing(service => new List<ServiceHost>()
                    {
                         new ServiceHost(typeof (AsansörveIşıklar .AsansorveIsıklar)),
                        new ServiceHost (typeof (UserService.UserPermissionService )),
                        new ServiceHost (typeof (UserService.UserService)),
                        new ServiceHost (typeof (Machine.MachineService)),
                        new ServiceHost (typeof (ITS_Service.UsersServiceITS)),
                    });
                    s.WhenStarted(service =>
                    {
                        service.ForEach(a =>
                        {

                            a.Open();
                            Console.WriteLine($"\n{string.Join("\n", a.ChannelDispatchers.Select(b => b.Listener.Uri))}\n{a.BaseAddresses[0].ToString().Split('/').Last()}Host has been Started {System.DateTime.UtcNow.ToShortDateString()}".AppendLog());

                        });
                    });
                    s.WhenStopped(service =>
                    {
                        service.ForEach(a =>
                        {
                            a.Close();
                            Console.WriteLine($"{a.ToString()} Host has Closed {System.DateTime.UtcNow.ToShortDateString()}".AppendLog());

                        });
                    });

                });

   

                x.RunAsLocalSystem();
                x.SetDescription("Winpack Wcf Servisleri");
                x.SetServiceName("WcfWinpackService");
                x.SetDisplayName("WcfWinpackService");

            });
            int ExitCodevalue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = ExitCodevalue;
            Console.ReadLine();
        }

        ServiceHost CustomService(Type ServiceType, Type İmplementContract, Uri[] Uri, System.ServiceModel.Channels.Binding binding)
        {
            ServiceHost yeni = new ServiceHost(ServiceType, Uri);
            yeni.AddServiceEndpoint(İmplementContract, binding, "");
            return yeni;
        }

    }
}
