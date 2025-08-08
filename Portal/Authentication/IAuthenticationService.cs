using Portal.Authentication.Models;

namespace Portal.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> LoginAsync(AuthenticationUserModel userForAuthentication);
        Task LogoutAsync();
    }
}