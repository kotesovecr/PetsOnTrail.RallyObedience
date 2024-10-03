namespace RallyObedienceApp.Persistency.Models;

internal sealed record ActionItem : IItem
{
    public string ID { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Address { get; init; } = null!;
    public bool Done { get; set; } = false;
}
