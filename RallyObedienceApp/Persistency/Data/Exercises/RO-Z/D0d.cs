using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class D0d
{
    public const string ID = "D0d";
    public const string Name = "Přiřazení přímo - vpřed";
    public const string Description = "Pes z pozice před psovodem se přiřazuje přímo k levé noze (neobchází) a při dosažení úrovně jeho levé nohy (pes si nesedá) společně pokračují v přímém směru. Během přiřazování psa psovod nesmí hýbat nohama.<br />Hodnocení je zahrnuto do bodového hodnocení cviku na hlavní kartě.";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "přiřazení přímo - vpřed", IsMain = false }
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "D0d",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0006.svg"
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
