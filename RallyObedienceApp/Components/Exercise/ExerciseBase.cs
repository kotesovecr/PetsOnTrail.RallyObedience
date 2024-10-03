using Microsoft.AspNetCore.Components;
using RallyObedienceApp.Persistency;
using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Components.Exercise;

public class ExerciseBase : ComponentBase
{
    [Parameter] public string Id { get; set; } = string.Empty;
    [Inject] protected ExerciseDbService DbService { get; set; }

    protected ExerciseItem Exercise { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Exercise = await DbService.GetItemAsync(Id);
    }
}
