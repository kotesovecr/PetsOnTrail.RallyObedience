using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OpenTK.Graphics.ES10;
using RallyObedienceApp.Persistency;
using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Components.ParkourView;

public class ParkourViewBase : ComponentBase
{
    [Parameter] public string Id { get; set; } = string.Empty;
    [Inject] protected ParkourDbService DbService { get; set; }
    [Inject] protected ExerciseDbService ExerciseDbService { get; set; }
    [Inject] protected IJSRuntime JSRuntime { get; set; }
    IJSObjectReference module;

    protected ParkourItem? Parkour { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Parkour = await DbService.GetItemAsync(Id);

        if (Parkour is not null)
        {
            var exerciseDictionary = new Dictionary<string, string>();
            var exercises = Parkour.Positions.SelectMany(p => p.Exercises).Select(e => e.ExerciseId).Distinct();
            foreach (var exerciseId in exercises)
            {
                exerciseDictionary[exerciseId] = (await ExerciseDbService.GetItemAsync(exerciseId))?.Image;
            }

            var positions = Parkour?.Positions.Select(p => new { y = p.Top, x = p.Left, exercises = p.Exercises.Select(e => new { src = exerciseDictionary[e.ExerciseId] })});

            // mPx - meter per pixels - 1m = 50px
            // width of parkour = 20m
            // height of parkour = 20m
            await module.InvokeVoidAsync("drawParkour", 50, 20, 20, positions);
        }
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
