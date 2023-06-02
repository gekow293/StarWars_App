using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarWars_App.Models.ViewModels;

public class CharacterVM
{
    public CharacterVM() { }
    public CharacterVM(Character character)
    {
        Id = character.Id;
        Name = character.Name;
        NameOrign = character.NameOrign;
        Birthday = character.Birthday;
        Planet = character.Planet;
        Sex = character.Sex;
        Race = character.Race;
        HairColor = character.HairColor;
        Height = character.Height;
        EyeColor = character.EyeColor;
        Description = character.Description;
        SelectedFilms = character?.Films?.Select(x => x.FilmId).ToList();
        UserIdCreate = character.UserIdCreate;
    }

    [Key]
    public int Id { get; set; }
    [Required]
    [DisplayName("Имя персонажа")]
    public string? Name { get; set; }
    [Required]
    [DisplayName("Имя (в оригинале)")]
    public string? NameOrign { get; set; }
    [DisplayName("Дата рождения")]
    public string? Birthday { get; set; }
    [Required]
    [DisplayName("Планета")]
    public string? Planet { get; set; }
    [Required]
    [DisplayName("Пол")]
    public string? Sex { get; set; }
    [DisplayName("Раса")]
    public string? Race { get; set; }
    [DisplayName("Рост")]
    public float Height { get; set; }
    [Required]
    [DisplayName("Цвет волос")]
    public string? HairColor { get; set; }
    [Required]
    [DisplayName("Цвет глаз")]
    public string? EyeColor { get; set; }
    [DisplayName("Описание")]
    public string? Description { get; set; }
    public IEnumerable<SelectListItem>? CharacterFilms { get; set; }
    [DisplayName("Фильмы")]
    public List<int>? SelectedFilms { get; set; } = new();
    public string? UserIdCreate { get; set; }
}
