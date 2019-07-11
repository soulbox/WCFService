namespace WcfServiceHost.ITS_Service
{
    using Entity;
    using Repository.ITSRepo;
    using DTO.ITSDTO;
    using System.ServiceModel;

    public class BildirimTipiService : ITS_ServiceBase<BildirimTipiRepo, BildirmTipi, BildirmTipiDTO>, IBildirimTipiService
    {

    }
    [ServiceContract]
    public interface IBildirimTipiService : IServiceBase<BildirmTipiDTO>
    {

    }
}
