namespace Proxy
{
    public class AuthenticationController : IAuthenticationController
    {
        private IUser _user;
        public AuthenticationController(IUser user)
        {
            _user = user;
        }
        public bool IsAdmin()
        {
            return _user.IsAdmin;
        }
    }
}
