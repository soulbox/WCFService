namespace WcfServiceHost.UserService
{
    using Repository;
    using Entity;
    using DTO.DataCollect;
    using System.ServiceModel;
    public class UserService:ServiceBase<UserRepository,User,UserDTO>,IUserService
    {

    }
    [ServiceContract]
    public interface IUserService :IServiceBase<UserDTO>
    {

    }
}
