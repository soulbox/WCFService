using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfServiceHost
{
    using Extensions;
    using Repository;
    public class ServiceBase<Rep, Ent, DTO> : IServiceBase<DTO>
        where Rep : IRepository<Ent>
        where Ent : class
        where DTO : class
    {
        static Rep _repository;

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

        public   bool Ekle(DTO entity)
        {
            return Repository.Ekle(entity.MapTo<Ent>());
        }

        public bool Güncelle(DTO entity)
        {
            return Repository.Güncelle(entity.MapTo<Ent>());
        }

        public List<DTO> Listele()
        {

            return Repository.Listele().Select(x => x.MapTo<DTO>()).ToList();

        }

        public bool Sil(DTO entity)
        {
            return Repository.Sil(entity.MapTo<Ent>());

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
        bool Ekle(DTO entity);
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool Güncelle(DTO entity);
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool Sil(DTO entity);
    }
}
