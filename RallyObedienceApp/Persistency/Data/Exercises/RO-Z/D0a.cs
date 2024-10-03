using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class D0a
{
    public const string ID = "D0a";
    public const string Name = "Přiřazení okolo - stop";
    public const string Description = "Pes z pozice před psovodem jej oběhne z pravé strany a přiřadí se do základní pozice u levé nohy. Během pohybu psa psovod nesmí hýbat nohama. Hodnocení je zahrnuto do bodového hodnocení cviku na hlavní kartě.";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "přiřazení okolo- stop", IsMain = false }
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "D0a",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0003.svg"
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
