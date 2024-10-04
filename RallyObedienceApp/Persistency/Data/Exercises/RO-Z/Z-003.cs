using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class Z_003
{
    public const string ID = "Z-003";
    public const string Name = "Stop – lehni – sedni";
    public const string Description = "Tým zastaví u karty v místě pro vykonání cviku a zaujme základní pozici. Na povel psovoda si pes lehá. Na další povel psovoda si pes sedá.";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "sedni", IsMain = false },
        new ExercisePartial { Exercise = "lehni", IsMain = false },
        new ExercisePartial { Exercise = "sedni", IsMain = false },
        new ExercisePartial { Exercise = "po zaujetí každé pozice psovod stojí vedle psa", IsMain = false }
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "003",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0010.svg"
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
