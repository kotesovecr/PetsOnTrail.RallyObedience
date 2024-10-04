using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class D0b
{
    public const string ID = "D0b";
    public const string Name = "Přiřazení přímo - stop";
    public const string Description = "Pes z pozice před psovodem se přiřazuje přímo k levé noze psovoda a zaujímá základní pozici. Během pohybu psa psovod nesmí hýbat nohama. Hodnocení je zahrnuto do bodového hodnocení hlavního cviku";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "přiřazení přímo - stop", IsMain = false }
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "D0b",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0004.svg"
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
