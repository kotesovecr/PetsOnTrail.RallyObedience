using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Exercises.RO_Z;

internal static class Finish
{
    public const string ID = "Finish";
    public const string Name = "Finish";
    public const string Description = "Posuzování končí po překročení cílové linie. Pak může psovod psa vydatně pochválit a odměnit (pohlazení, hra, pamlsky či hračka).";
    public static List<ExercisePartial> Partials => new()
    {
    };

    public static ExerciseItem CreateMain() => new()
    {
        ID = ID,
        Category = "Z",
        Number = "Finish",
        Name = Name,
        Description = Description,
        Partials = Partials,
        Image = "/imgs/exercises/ROZ/0002.svg",
    };

    public static List<ExercisePartial> CreatePartials() => Partials.Select(partial => new ExercisePartial
    {
        ExerciseID = ID,
        Exercise = partial.Exercise,
        IsMain = partial.IsMain
    }).ToList();
}
