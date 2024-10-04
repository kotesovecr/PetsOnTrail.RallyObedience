using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RallyObedienceApp.Persistency;
using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Components.ParkourView;

public class ParkourViewBase : ComponentBase
{
    [Parameter] public string Id { get; set; } = string.Empty;
    [Inject] protected ParkourDbService DbService { get; set; }
    [Inject] protected IJSRuntime JSRuntime { get; set; }
    IJSObjectReference module;

    protected ParkourItem? Parkour { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Parkour = await DbService.GetItemAsync(Id);

        await module.InvokeVoidAsync("drawParkour", 20, 20);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/ParkourView.js");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
