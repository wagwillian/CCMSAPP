using Portal.Authentication.Models;

namespace Portal.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}