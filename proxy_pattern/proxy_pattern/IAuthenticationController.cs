namespace ProxyPattern
{
    public interface IAuthenticationController
    {
        bool Login(IUser user);
    }
}