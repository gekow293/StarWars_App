using StarWars_App.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace StarWars_App.Models;

public class Film
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public List<CharacterFilm>? Characters { get; set; } = new();
}
