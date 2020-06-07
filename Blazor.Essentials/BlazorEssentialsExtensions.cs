namespace Blazor.Essentials
{
    using Blazor.Essentials.IntersectionObserverAPI;
    using Blazor.Essentials.ResizeObserverAPI;
    using Blazor.Essentials.WebStorage;
    using Microsoft.Extensions.DependencyInjection;

    public static class BlazorEssentialsExtensions
    {
        public static IServiceCollection AddBlazorEssentials(this IServiceCollection services)
        {
            return services
                .AddSingleton<IResizeObserverFactory, ResizeObserverFactory>()
                .AddSingleton<IIntersectionObserverFactory, IntersectionObserverFactory>()
                .AddSingleton<ILocalStorage, LocalStorage>()
                .AddSingleton<ISessionStorage, SessionStorage>();
        }
    }
}
