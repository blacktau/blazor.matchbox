namespace Blazor.Matchbox
{
    using Blazor.Matchbox.IntersectionObserverAPI;
    using Blazor.Matchbox.ResizeObserverAPI;
    using Blazor.Matchbox.WebStorage;

    using Microsoft.Extensions.DependencyInjection;

    public static class BlazorMatchboxExtensions
    {
        public static IServiceCollection AddBlazorMatchbox(this IServiceCollection services)
        {
            return services
                .AddSingleton<IResizeObserverFactory, ResizeObserverFactory>()
                .AddSingleton<IIntersectionObserverFactory, IntersectionObserverFactory>()
                .AddSingleton<ILocalStorage, LocalStorage>()
                .AddSingleton<ISessionStorage, SessionStorage>();
        }
    }
}
