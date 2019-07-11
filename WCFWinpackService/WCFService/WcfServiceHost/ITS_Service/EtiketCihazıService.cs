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

    public class EtiketCihazıService : ITS_ServiceBase<EtiketCihazıRepo , EtiketCihazı , EtiketCihazıDTO>, IEtiketCihazıService
    {

    }
    [ServiceContract]
    public interface IEtiketCihazıService : IServiceBase<EtiketCihazıDTO>
    {

    }
}
