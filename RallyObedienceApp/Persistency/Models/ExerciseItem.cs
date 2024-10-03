using SQLite;
using SQLiteNetExtensions.Attributes;

namespace RallyObedienceApp.Persistency.Models;

public class ExerciseItem
{
    [PrimaryKey]
    public string ID { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<ExercisePartial> Partials { get; set; } = new();
}

public class ExercisePartial
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [ForeignKey(typeof(ExerciseItem))]
    public string ExerciseID { get; set; } = string.Empty;
    public string Exercise { get; set; } = string.Empty;
    public bool IsMain { get; set; } = false;
}
