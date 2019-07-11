namespace WcfServiceHost.UserService
{
    using Repository;
    using Entity;
    using DTO.DataCollect;
    using System.ServiceModel;
    public class UserPermissionService : ServiceBase<UserPermissionRepository, UserPermission, UserPermissionDTO>, IUserPermissionService
    {

    }
    [ServiceContract]
    public interface IUserPermissionService : IServiceBase<UserPermissionDTO>
    {
    }
}
