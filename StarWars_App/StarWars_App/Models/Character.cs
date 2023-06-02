using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Numerics;
using StarWars_App.Models;

namespace StarWars_App.Models;

public class Character
{
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
    [DisplayName("Фильмы")]
    public List<CharacterFilm>? Films { get; set; } = new();
    public string? UserIdCreate { get; set; }
}
