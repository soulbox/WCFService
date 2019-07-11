using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceHost.ITS_Service
{
    using Extensions;
    using Repository;
    using System.ServiceModel;
    using System.ServiceModel.Web;
   
    public class ITS_ServiceBase<Rep, Ent, DTO> : IServiceBase<DTO>
        where Rep : IRepository<Ent>
        where Ent : class
        where DTO : class
    {

        static Rep _repository;
        MethodResult res;
        public ITS_ServiceBase()
        {
            res = res ?? new MethodResult();
        }
        public static Rep Repository
        {
            get
            {
                
                if (_repository == null)
                    _repository = Activator.CreateInstance<Rep>();
                return _repository;
            }

            set
            {
                _repository = value;
            }
        }
        public virtual MethodResult Ekle(DTO entity)
        {
            if (Repository.Ekle(entity.MapTo<Ent>()))
            {
                res.Status = Status.Success;                
                return res;
            }
            else
            {
                res.Status = Status.Error;
                res.Result = "Eklenemedi.";
                return res;
            }

            
        }

        public virtual MethodResult Güncelle(DTO entity)
        {
            if (Repository.Güncelle (entity.MapTo<Ent>()))
            {
                res.Status = Status.Success;
                return res;
            }
            else
            {
                res.Status = Status.Error;
                res.Result = "Güncellenemedi.";
                return res;
            }
        }

        public virtual  List<DTO> Listele()
        {
            return Repository.Listele().Select(x => x.MapTo<DTO>()).ToList();
        }

        public virtual MethodResult Sil(DTO entity)
        {
            if (Repository.Sil(entity.MapTo<Ent>()))
            {
                res.Status = Status.Success;
                return res;
            }
            else
            {
                res.Status = Status.Error;
                res.Result = "Silinemedi.";
                return res;
            }
        }
    }

    [ServiceContract]
    public interface IServiceBase<DTO>
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<DTO> Listele();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        MethodResult Ekle(DTO entity);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        MethodResult Güncelle(DTO entity);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        MethodResult Sil(DTO entity);
    }
}
