using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class Z_004
{
    public const string ID = "Z-004";
    public const string Name = "Stop – okolo psa";
    public const string Description = "Tým zastaví u karty v místě pro vykonání cviku a zaujme základní pozici. Psovod může dát povel k setrvání na místě a obejde sedícího psa zepředu a zastaví se opět v základní pozici, kde krátce setrvá. Pes přitom nesmí změnit polohu ani pozici.";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "sedni", IsMain = true },
        new ExercisePartial { Exercise = "psovod okolo psa, pes zůstává sedět ", IsMain = true },
        new ExercisePartial { Exercise = "psovod stojí po ukončení vedle psa", IsMain = false }
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "004",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0011.svg"
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
