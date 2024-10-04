using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class D0c
{
    public const string ID = "D0c";
    public const string Name = "Přiřazení okolo - vpřed";
    public const string Description = "Pes z pozice před psovodem jej oběhne z pravé strany a při dosažení úrovně jeho levé nohy (pes si nesedá) společně pokračují v přímém směru. Během obíhání psem nesmí psovod hýbat nohama.<br />Hodnocení je zahrnuto do bodového hodnocení cviku na hlavní kartě.";
    public static List<ExercisePartial> Partials => new()
    {
        new ExercisePartial { Exercise = "přiřazení okolo - vpřed", IsMain = false }
    };
    public const bool Done = true;

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "D0c",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0005.svg"
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
