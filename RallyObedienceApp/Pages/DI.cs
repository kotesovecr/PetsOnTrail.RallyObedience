using RallyObedienceApp.Components.Pages.Parkours;

namespace RallyObedienceApp.Pages;

public static class DI
{
    public static IServiceCollection AddPages(this IServiceCollection services)
    {
        services
            .AddSingleton<ParkoursBase>();

        return services;
    }
}
