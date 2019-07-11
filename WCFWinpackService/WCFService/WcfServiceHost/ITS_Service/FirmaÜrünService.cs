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

    public class FirmaÜrünService : ITS_ServiceBase<FirmaÜrünRepo, FirmaÜrün , FirmaÜrünDTO >, IFirmaÜrünService
    {

    }
    [ServiceContract]
    public interface IFirmaÜrünService : IServiceBase<FirmaÜrünDTO>
    {

    }
}
