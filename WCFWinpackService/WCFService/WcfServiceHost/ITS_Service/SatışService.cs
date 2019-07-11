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

    public class SatışService : ITS_ServiceBase<SatışRepo, Satış, SatışDTO>, ISatışService
    {

    }
    [ServiceContract]
    public interface ISatışService : IServiceBase<SatışDTO>
    {

    }
}
