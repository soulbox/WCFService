using System.Collections.Generic;

namespace WcfServiceHost.Machine
{
    using DTO.DataCollect;
    using Repository;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System;

    public class MachineService : IMachineService
    {
        static MachineRepository mac;
        public MachineRepository Mac => mac = mac ?? new MachineRepository();
        public List<DB_Machine> Listele()
        {
            return Mac.Listele();
        }


    }
    [ServiceContract]
    public interface IMachineService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        List<DB_Machine> Listele();

    }


}
