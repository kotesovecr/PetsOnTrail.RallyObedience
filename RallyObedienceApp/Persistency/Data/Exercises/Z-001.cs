using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises;

internal static class Z_001
{
    public const string ID = "Z-001";
    public const string Name = "Stop";
    public const string Description = "Tým se zastaví u karty v místě pro provedení cviku a zaujme základní pozici. (pes si může sednout sám či na povel)";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "sedni", IsMain = true },
        new ExercisePartial { Exercise = "psovod stojí vedle psa", IsMain = false }
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = "Z-001",
        Category = "Z",
        Number = "001",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0008.svg",
        Done = Done
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
