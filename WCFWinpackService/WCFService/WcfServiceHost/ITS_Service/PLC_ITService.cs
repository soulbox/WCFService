using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceHost.ITS_Service
{

    using Entity;
    using Repository.ITSRepo;
    using DTO.ITSDTO;
    using System.ServiceModel;

    public class PLC_ITService : ITS_ServiceBase<PLCRepo, PLC, PLCDTO>, IPLC_ITService
    {

    }
    [ServiceContract]
    public interface IPLC_ITService : IServiceBase<PLCDTO>
    {

    }
}
