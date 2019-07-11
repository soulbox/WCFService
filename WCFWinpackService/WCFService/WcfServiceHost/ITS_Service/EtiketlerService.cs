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

    public class EtiketlerService : ITS_ServiceBase<EtiketlerRepo, Etiketler, EtiketlerDTO>, IEtiketlerService
    {

    }
    [ServiceContract]
    public interface IEtiketlerService : IServiceBase<EtiketlerDTO>
    {

    }
}
