using Microsoft.AspNetCore.Components;
using RallyObedienceApp.Persistency;
using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Components.ParkourView;

public class ParkourViewBase : ComponentBase
{
    [Parameter] public string Id { get; set; } = string.Empty;
    [Inject] protected ParkourDbService DbService { get; set; }

    protected ParkourItem? Parkour { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Parkour = await DbService.GetItemAsync(Id);
    }
}
