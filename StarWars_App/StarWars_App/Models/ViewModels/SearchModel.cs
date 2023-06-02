namespace StarWars_App.Models.ViewModels;

public class SearchModel
{
    public List<CharacterVM>? Characters { get; set; }
    public List<int>? Films { get; set; }
    public string? Planet { get; set; }
    public string? Sexes { get; set; }
    public int? BirthdayFrom { get; set; }
    public int? BirthdayTo { get; set; }
}
