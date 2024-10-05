using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
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

    private static Func<string, string, string, double, double, Task>? AddParkourExerciseInternalAsync;
    private static Func<int, string, string, double, double, Task>? UpdateParkourExerciseInternalAsync;
    private static Func<int, string, string, double, double, Task>? DeleteParkourExerciseInternalAsync;

    protected ParkourItem? Parkour { get; set; }

    protected override async Task OnInitializedAsync()
    {
        AddParkourExerciseInternalAsync = LocalAddParkourExerciseInternalAsync;
        UpdateParkourExerciseInternalAsync = LocalUpdateParkourExerciseInternalAsync;
        DeleteParkourExerciseInternalAsync = LocalDeleteParkourExerciseInternalAsync;

        Parkour = await DbService.GetItemAsync(Id);

        if (Parkour is not null)
        {
            var exercises = await ExerciseDbService.GetCategoryAsync("Z");

            // mPx - meter per pixels - 1m = 50px
            // width of parkour = 20m
            // height of parkour = 20m
            // await module.InvokeVoidAsync("drawParkour", 50, 20, 20, Parkour, false, exercises);
            await module.InvokeVoidAsync("drawParkour", 50, 20, 20, Parkour, true, exercises);
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

    [JSInvokable]
    public static async Task<int> AddParkourExerciseAsync(string id, string exerciseId, string number, double x, double y)
    {
        if (AddParkourExerciseInternalAsync is { } actionAsync)
        {
            await actionAsync(id, exerciseId, number, x, y);
        }

        return 0;
    }

    [JSInvokable]
    public static async Task<int> UpdateParkourExerciseAsync(int positionId, string exerciseId, string number, double x, double y)
    {
        if (UpdateParkourExerciseInternalAsync is { } actionAsync)
        {
            await actionAsync(positionId, exerciseId, number, x, y);
        }

        return 0;
    }

    [JSInvokable]
    public static async Task<int> DeleteParkourExerciseAsync(int positionId, string exerciseId, string number, double x, double y)
    {
        if (DeleteParkourExerciseInternalAsync is { } actionAsync)
        {
            await actionAsync(positionId, exerciseId, number, x, y);
        }

        return 0;
    }

    private async Task LocalAddParkourExerciseInternalAsync(string id, string exerciseId, string number, double left, double top)
    {
        if (left < 0)
            left = 0;

        if (top < 0)
            top = 0;

        Parkour?.Positions.Add(new PositionDto
        {
            Exercises = new List<PositionExercises>
            {
                new PositionExercises
                {
                    ID = id,
                    ExerciseId = exerciseId,
                    Number = number,
                    PositionID = Parkour.Positions.Count + 1
                }
            },
            ID = Parkour.Positions.Count + 1,
            Left = left,
            ParkourID = Parkour.ID,
            Rotation = 0.0,
            Top = top
        });

        await DbService.UpdateItemAsync(Parkour);
    }

    private async Task LocalUpdateParkourExerciseInternalAsync(int positionId, string exerciseId, string number, double left, double top)
    {
        if (left < 0)
            left = 0;

        if (top < 0)
            top = 0;

        var exercise = Parkour?.Positions.FirstOrDefault(p => p.ID == positionId)?.Exercises.FirstOrDefault(e => e.ExerciseId == exerciseId);
        if (exercise is not null)
        {
            exercise.Number = number;
        }

        var position = Parkour?.Positions.FirstOrDefault(p => p.ID == positionId);
        if (position is not null)
        {
            position.Left = left;
            position.Top = top;
        }


        await DbService.UpdateItemAsync(Parkour);
    }

    private async Task LocalDeleteParkourExerciseInternalAsync(int positionId, string exerciseId, string number, double left, double top)
    {
        Parkour?.Positions.FirstOrDefault(p => p.ID == positionId)?.Exercises.RemoveAll(e => e.ExerciseId == exerciseId && e.Number == number);
        if (Parkour?.Positions.FirstOrDefault(p => p.ID == positionId)?.Exercises.Count == 0)
        {
            Parkour?.Positions.RemoveAll(p => p.ID == positionId);
        }

        await DbService.UpdateItemAsync(Parkour);
    }
}
