using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class Start
{
    public const string ID = "Start";
    public const string Name = "Start";
    public const string Description = "Tým nemusí zaujmout žádnou základní pozici a může hned po přípravě startovat. Posuzování začíná po překročení startovní linie normálním tempem.<br />Pokud parkur začíná rovnou na pravou ruku, je karta start doplněna značkou / označením „R“.";
    public static List<ExercisePartial> Partials => new()
    {
    };

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "",
        Number = "START",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0001.svg",
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
