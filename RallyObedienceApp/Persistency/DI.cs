namespace RallyObedienceApp.Persistency;

internal static class DI
{
    public static IServiceCollection AddPersistency(this IServiceCollection services)
    {
        services
            .AddSingleton<ExerciseDbService>()
            .AddSingleton<ParkourDbService>();

        return services;
    }
}
