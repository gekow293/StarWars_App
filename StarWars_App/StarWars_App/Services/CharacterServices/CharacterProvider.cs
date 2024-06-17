using Microsoft.EntityFrameworkCore;
using StarWars_App.Data;
using StarWars_App.Models;

namespace StarWars_App.Services.CharacterServices;

public class CharacterProvider : ICharacterProvider
{
    private readonly ApplicationContext db;

    public CharacterProvider(ApplicationContext db)
    {
        this.db = db;
    }
    public async Task<IEnumerable<Character>?> GetAllCharactersAsync()
    {
        return await db.Characters.ToListAsync();
    }

    public async Task<Character?> GetCharacterAsync(int id)
    {
        return await db.Characters.FindAsync(id);
    }
}
