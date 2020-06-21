namespace Blazor.Matchbox
{
    using Blazor.Matchbox.Observers.Intersection;
    using Blazor.Matchbox.Observers.Resize;
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
