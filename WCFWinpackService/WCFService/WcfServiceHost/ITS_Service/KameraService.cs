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

    public class KameraService : ITS_ServiceBase<KameraRepo , Kamera , KameraDTO >, IKameraService
    {

    }
    [ServiceContract]
    public interface IKameraService : IServiceBase<KameraDTO>
    {

    }
}
