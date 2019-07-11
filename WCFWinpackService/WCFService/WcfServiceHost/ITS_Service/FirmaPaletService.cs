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

    public class FirmaPaletService : ITS_ServiceBase<FirmaPaletRepo, FirmaPalet, FirmaPaletDTO >, IFirmaPaletService
    {

    }
    [ServiceContract]
    public interface IFirmaPaletService : IServiceBase<FirmaPaletDTO>
    {

    }
}
