using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StarWars_App.Data;
using StarWars_App.Models;
using StarWars_App.Models.DTO;
using StarWars_App.Models.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Claims;

namespace StarWars_App.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationContext db;

    public HomeController(ApplicationContext db)
    {
        this.db = db;
    }

    // GET
    public ActionResult Index()
    {
        SearchModel model = new SearchModel();

        model.Characters = db.Characters.ToArray().OrderBy(x => x.Name).Select(x => new CharacterVM(x)).ToList();

        ViewBag.Films = db.Films.ToArray().Select(o => new SelectListItem
        {
            Text = o.Name,
            Value = o.Id.ToString()
        } );

        ViewBag.Planets = db.Characters.ToArray().ToLookup(x => x.Planet).Select(y => y.FirstOrDefault()).Select(o => new SelectListItem
        {
            Text = o?.Planet,
            Value = o?.Planet
        } );

        ViewBag.Sexes = db.Characters.ToArray().ToLookup(x => x.Sex).Select(y => y.FirstOrDefault()).Select(o => new SelectListItem
        {
            Text = o?.Sex,
            Value = o?.Sex
        });

        return View(model);
    }

    [HttpPost]
    public ActionResult Search(SearchModel model)
    {
        model.Characters = db.Characters.ToArray().OrderBy(x => x.Name).Select(x => new CharacterVM(x)).ToList();

        var serchCharacters = new List<CharacterVM>();

        if (model.Films != null)
            foreach (var character in model.Characters)
            {
                var filmsOfCh = db.CharacterFilms.Where(x => x.CharacterId == character.Id).Select(o => o.FilmId).ToList();

                if (!model.Films.Except(filmsOfCh).Any()) serchCharacters.Add(character);     
            }

        model.Characters = serchCharacters;

        if (!string.IsNullOrEmpty(model.Planet))
            model.Characters = model.Characters.Where(x => x.Planet == model.Planet).ToList();

        if (!string.IsNullOrEmpty(model.Sexes))
            model.Characters = model.Characters.Where(x => x.Sex == model.Sexes).ToList();

        if (!string.IsNullOrEmpty(model.BirthdayFrom.ToString()))
            model.Characters = model.Characters.Where(x => Convert.ToInt32(x.Birthday) >= Convert.ToInt32(model.BirthdayFrom)).ToList();

        if (!string.IsNullOrEmpty(model.BirthdayTo.ToString()))
            model.Characters = model.Characters.Where(x => Convert.ToInt32(x.Birthday) <= Convert.ToInt32(model.BirthdayTo)).ToList();

        return PartialView("Search", model);
    }

    // GET
    public ActionResult AddCharacter()
    {
        var modelCharacter = new CharacterVM();
        var allFilmsList = db.Films.ToList();
        modelCharacter.CharacterFilms = allFilmsList.Select(o => new SelectListItem
        { 
            Text = o.Name, 
            Value = o.Id.ToString() }
        );

        return View(modelCharacter);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddCharacter(CharacterVM modelCharacter)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var allFilmsList = db.Films.ToList();
                modelCharacter.CharacterFilms = allFilmsList.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                } );
                return View(modelCharacter);
            }

            if (db.Characters!.Any(x => x.Name == modelCharacter.Name))
            {
                var allFilmsList = db.Films.ToList();
                modelCharacter.CharacterFilms = allFilmsList.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                });
                ModelState.AddModelError("", "Герой с таким именем уже существует, измените имя на уникальное");
                return View(modelCharacter);
            }

            var character = new Character()
            {
                Name = modelCharacter.Name,
                NameOrign = modelCharacter.NameOrign,
                Birthday = modelCharacter.Birthday,
                Planet = modelCharacter.Planet,
                Sex = modelCharacter.Sex,
                Race = modelCharacter.Race,
                Height = modelCharacter.Height,
                HairColor = modelCharacter.HairColor,
                EyeColor = modelCharacter.EyeColor,
                Description = modelCharacter.Description,
                UserIdCreate = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value
        };

            if(modelCharacter.SelectedFilms?.Count > 0)
                foreach (var filmId in modelCharacter.SelectedFilms)
                    character.Films?.Add(new CharacterFilm() { CharacterId = modelCharacter.Id, FilmId = filmId });

            db.Characters?.AddAsync(character);
            await db.SaveChangesAsync();

            TempData["SM"] = "Вы добавили героя";
            return RedirectToAction(nameof(AddCharacter));
        }
        catch
        {
            return View(modelCharacter);
        }
    }

    // GET
    public ActionResult EditCharacter(int id)
    {
        CharacterVM? modelCharacter;
        Character? character = db.Characters?.Find(id);

        if (character?.UserIdCreate == User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value)
        {
            if (character == null)
            {
                return Content("Этого героя не существует");
            }

            modelCharacter = new CharacterVM(character);

            var characterFilms = db.CharacterFilms.Where(x => x.CharacterId == id).Select(x => x.Film).ToList();
            modelCharacter.CharacterFilms = characterFilms?.Select(o => new SelectListItem
            {
                Text = o?.Name,
                Value = o?.Id.ToString()
            });

            return View(modelCharacter);
        }
        else
        {
            TempData["SM"] = "Редактирование персонажа разрешено только его создателю";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditCharacter(CharacterVM model)
    {
        try
        {
            var character = db.Characters?.Find(model.Id);

            if (character != null)
            {
                character.Name = model.Name;
                character.NameOrign = model.NameOrign;
                character.Birthday = model.Birthday;
                character.Planet = model.Planet;
                character.Sex = model.Sex;
                character.Race = model.Race;
                character.Height = model.Height;
                character.HairColor = model.HairColor;
                character.EyeColor = model.EyeColor;
                character.Description = model.Description;

                if (model.SelectedFilms?.Count > 0)
                    foreach (var i in model.SelectedFilms) 
                    {
                        var film = new CharacterFilm() { CharacterId = model.Id, FilmId = i };
                        if (db.CharacterFilms.Contains(film))
                            db.CharacterFilms.Remove(film);
                    }

                await db.SaveChangesAsync();

                TempData["SM"] = "Этот изменили персонажа";

                return RedirectToAction(nameof(Index));
            }
            else TempData["SM"] = "Этот персонаж удален или изменен";

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            TempData["SM"] = "Ошибка!";
            return View(model);
        }
    }

    // GET
    public async Task<ActionResult> DeleteCharacter(int id)
    {
        var character = db.Characters.Find(id);

        if (character?.UserIdCreate == User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value)
        {
            if (character != null)
            {
                db.Characters.Remove(character);
                await db.SaveChangesAsync();

                TempData["SM"] = "Вы удалили персонажа";

                return RedirectToAction(nameof(Index));
            }
            else TempData["SM"] = "Этот персонаж удален или изменен";

            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["SM"] = "Удаление персонажа разрешено только его создателю";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET
    public ActionResult CharacterInfo(int id)
    {
        try
        {
            var character = db.Characters.Find(id);

            if (character != null)
            {
                var modelCharacter = new CharacterVM(character);

                var characterFilms = db.CharacterFilms.Where(x => x.CharacterId == id).Select(x => x.Film).ToList();
                modelCharacter.CharacterFilms = characterFilms?.Select(o => new SelectListItem
                {
                    Text = o?.Name,
                    Value = o?.Id.ToString()
                });

                return View(modelCharacter);
            }
            else
            {
                TempData["SM"] = "Этот персонаж удален или изменен";

                return RedirectToAction(nameof(Index));
            }
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}