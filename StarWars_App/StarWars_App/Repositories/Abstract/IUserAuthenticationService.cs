using StarWars_App.Models.DTO;

namespace StarWars_App.Repositories.Abstract;

public interface IUserAuthenticationService
{
    Task<Status> LoginAsync(LoginModel model);
    Task LogoutAsync();
    Task<Status> RegisterAsync(RegistrationModel model);
}
