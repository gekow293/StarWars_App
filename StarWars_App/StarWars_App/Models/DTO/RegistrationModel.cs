using System.ComponentModel.DataAnnotations;

namespace StarWars_App.Models.DTO;

public class RegistrationModel
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$",ErrorMessage ="Минимальная длина 6 символов и 1 в верхнем регистре,1 в нижнем, 1 специальный и 1 цифра")]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string PasswordConfirm { get; set; }
    public string? Role { get; set; }
}
