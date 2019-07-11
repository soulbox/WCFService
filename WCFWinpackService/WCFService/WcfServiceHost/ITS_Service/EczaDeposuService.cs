
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace WcfServiceHost.ITS_Service
{
    using DTO.ITSDTO;
    using Entity;
    using Repository.ITSRepo;
    using System.ServiceModel;
    public class EczaDeposuService : ITS_ServiceBase<EczaDeposuRepo, EczaDeposu, EczaDeposuDTO>, IEczaDeposuService
    {

    }
    [ServiceContract]
    public interface IEczaDeposuService : IServiceBase<EczaDeposuDTO>
    {

    }
}
