using StarWars_App.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWars_App.Models;

public class CharacterFilm
{
    public int FilmId { get; set; }
    public Film? Film { get; set; }
    public int CharacterId { get; set; }
    public Character? Character { get; set; }
}
