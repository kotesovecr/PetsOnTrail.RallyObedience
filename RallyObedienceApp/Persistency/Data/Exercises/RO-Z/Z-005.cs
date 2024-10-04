using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class Z_005
{
    public const string ID = "Z-005";
    public const string Name = "Stop – lehni – okolo psa";
    public const string Description = "Tým zastaví u karty v místě pro vykonání cviku a zaujme základní pozici. Na povel psovoda si pes lehá. Psovod může dát povel k setrvání na místě a obejde ležícího psa zepředu. Po obejití se zastaví vedle ležícího psa a krátce v pozici setrvá. Během pohybu psovoda pes nesmí změnit svoji polohu ani pozici.";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "sedni", IsMain = false },
        new ExercisePartial { Exercise = "lehni", IsMain = true },
        new ExercisePartial { Exercise = "psovod stojí po každé pozici vedle psa", IsMain = false },
        new ExercisePartial { Exercise = "psovod obchází psa, pes leží", IsMain = true },
        new ExercisePartial { Exercise = "psovod stojí po obejití vedle psa", IsMain = false },
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "005",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0012.svg"
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
