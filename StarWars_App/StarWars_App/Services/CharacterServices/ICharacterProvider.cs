using StarWars_App.Models;

namespace StarWars_App.Services.CharacterServices;

public interface ICharacterProvider
{
    Task<IEnumerable<Character>?> GetAllCharactersAsync();
    Task<Character?> GetCharacterAsync(int id);
}
