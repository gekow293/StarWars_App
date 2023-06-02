using StarWars_App.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StarWars_App.Models;

namespace StarWars_App.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Film> Films { get; set; } = null!;
    public DbSet<CharacterFilm> CharacterFilms { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CharacterFilm>()
           .HasKey(cf => new { cf.CharacterId, cf.FilmId });

        modelBuilder.Entity<CharacterFilm>()
           .HasOne(c => c.Film)
           .WithMany(f => f.Characters)
           .HasForeignKey(ci => ci.FilmId);

        modelBuilder.Entity<CharacterFilm>()
           .HasOne(f => f.Character)
           .WithMany(c => c.Films)
           .HasForeignKey(fi => fi.CharacterId);

        modelBuilder.Entity<Film>().HasData(
          new Film() { Id = 1, Name = "Эпизод IV — Новая надежда" },
          new Film() { Id = 2, Name = "Эпизод V — Империя наносит ответный удар" },
          new Film() { Id = 3, Name = "Эпизод VI — Возвращение джедая" },
          new Film() { Id = 4, Name = "Эпизод I — Скрытая угроза" },
          new Film() { Id = 5, Name = "Эпизод II — Атака клонов" },
          new Film() { Id = 6, Name = "Эпизод III — Месть ситхов" },
          new Film() { Id = 7, Name = "Эпизод VII — Пробуждение силы" },
          new Film() { Id = 8, Name = "Эпизод VIII — Последние джедаи" },
          new Film() { Id = 9, Name = "Эпизод IX — Скайуокер. Восход" }
          );

        modelBuilder.Entity<Character>().HasData(

           new Character
           {
               Id = 1,
               Name = "Йода",
               NameOrign = "Yoda",
               Birthday = "896",
               Description = "Описание",
               Sex = "Мужчина",
               Race = "Раса Йоды",
               Height = 0.66f,
               EyeColor = "Зеленый",
               HairColor = "Белый",
               Planet = "Дагоба"
           }
        );

        modelBuilder.Entity<CharacterFilm>().HasData(
            new CharacterFilm() { CharacterId = 1, FilmId = 2 },
            new CharacterFilm() { CharacterId = 1, FilmId = 3 },
            new CharacterFilm() { CharacterId = 1, FilmId = 4 },
            new CharacterFilm() { CharacterId = 1, FilmId = 6 },
            new CharacterFilm() { CharacterId = 1, FilmId = 7 },
            new CharacterFilm() { CharacterId = 1, FilmId = 8 },
            new CharacterFilm() { CharacterId = 1, FilmId = 9 }
            );
    }
}
