using Northwind.Contracts.AuthenticationWebAPI;
using Northwind.Contracts.Dto.AuthenticationWebAPI;

namespace BlazorWebApp.HttpRepository
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication);
        Task Logout();
    }
}
