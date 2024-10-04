using Microsoft.AspNetCore.Components;

namespace RallyObedienceApp.Components.Pages.Parkours;

public class ParkoursBase : ComponentBase
{
    [Parameter] public string Category { get; set; } = string.Empty;
    public List<string> ParkourIds { get; set; } = new();

    protected override void OnInitialized()
    {
        if (Category == string.Empty)
        {
            ParkourIds = new List<string> { };
        }
        else if (Category.ToUpper() == "RO-Z")
        {
            ParkourIds = new List<string> { "RO-Z-00001" /*, "RO-Z-00002", "RO-Z-00003"*/ };
        }
    }
}
