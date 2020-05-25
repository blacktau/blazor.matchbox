namespace Blazor.Essentials
{
    using Blazor.Essentials.ResizeObserverAPI;
    using Blazor.Essentials.ResizeObserverAPI.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    public static class BlazorEssentialsExtensions
    {
        public static IServiceCollection AddBlazorEssentials(this IServiceCollection services)
        {
            return services
                .AddSingleton<IResizeObserverFactory, ResizeObserverFactory>();
        }
    }
}
