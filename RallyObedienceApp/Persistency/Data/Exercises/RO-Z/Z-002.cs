using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class Z_002
{
    public const string ID = "Z-002";
    public const string Name = "Stop – lehni";
    public const string Description = "Tým se zastaví u karty v místě pro vykonání cviku a zaujme základní pozici. Na povel psovoda si pes lehá.";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "sedni", IsMain = false },
        new ExercisePartial { Exercise = "lehni", IsMain = true },
        new ExercisePartial { Exercise = "po zaujetí každé pozice psovod stojí vedle psa", IsMain = false }
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "002",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0009.svg"
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
