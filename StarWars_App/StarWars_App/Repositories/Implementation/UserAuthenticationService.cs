using StarWars_App.Models;
using StarWars_App.Models.Domain;
using StarWars_App.Models.DTO;
using StarWars_App.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace StarWars_App.Repositories.Implementation;

public class UserAuthenticationService: IUserAuthenticationService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    public UserAuthenticationService(UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.signInManager = signInManager; 

    }

    public async Task<Status> RegisterAsync(RegistrationModel model)
    {
        var status = new Status();
        var userExists = await userManager.FindByNameAsync(model.Username);
        if (userExists != null)
        {
            status.StatusCode = 0;
            status.Message = "Такой пользователь уже существует";
            return status;
        }
        ApplicationUser user = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username,
            FirstName = model.FirstName,
            LastName=model.LastName,
            EmailConfirmed=true,
            PhoneNumberConfirmed=true,
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "Ошибка создания пользователя";
            return status;
        }

        if (!await roleManager.RoleExistsAsync(model.Role))
            await roleManager.CreateAsync(new IdentityRole(model.Role));
        

        if (await roleManager.RoleExistsAsync(model.Role))
        {
            await userManager.AddToRoleAsync(user, model.Role);
        }

        status.StatusCode = 1;
        status.Message = "Вы успешно зарегистрировались";
        return status;
    }

    public async Task<Status> LoginAsync(LoginModel model)
    {
        var status = new Status();
        var user = await userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            status.StatusCode = 0;
            status.Message = "Неправильный Ник";
            return status;
        }

        if (!await userManager.CheckPasswordAsync(user, model.Password))
        {
            status.StatusCode = 0;
            status.Message = "Неправильный пароль";
            return status;
        }

        var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
        if (signInResult.Succeeded)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            status.StatusCode = 1;
            status.Message = "Вы успешно вошли в систему";
        }
        else if (signInResult.IsLockedOut)
        {
            status.StatusCode = 0;
            status.Message = "Пользователь заблокирован";
        }
        else
        {
            status.StatusCode = 0;
            status.Message = "Ошибка при входе";
        }
       
        return status;
    }

    public async Task LogoutAsync()
    {
       await signInManager.SignOutAsync();  
    }
}
