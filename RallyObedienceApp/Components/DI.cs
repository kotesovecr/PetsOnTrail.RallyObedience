using RallyObedienceApp.Components.Exercise;
using RallyObedienceApp.Components.ParkourView;

namespace RallyObedienceApp.Components;

internal static class DI
{
    public static IServiceCollection AddComponents(this IServiceCollection services)
    {
        services
            .AddSingleton<ExerciseBase>()
            .AddSingleton<ParkourViewBase>();

        return services;
    }
}
