using RallyObedienceApp.Components.Exercise;

namespace RallyObedienceApp.Components;

internal static class DI
{
    public static IServiceCollection AddComponents(this IServiceCollection services)
    {
        services
            .AddSingleton<ExerciseBase>();

        return services;
    }
}
