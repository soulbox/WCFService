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

    public class TransferService : ITS_ServiceBase<TransferRepo, Transfer, TransferDTO>, ITransferService
    {

    }
    [ServiceContract]
    public interface ITransferService : IServiceBase<TransferDTO>
    {

    }
}
