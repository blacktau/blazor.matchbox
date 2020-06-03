namespace Blazor.Essentials
{
    using Blazor.Essentials.IntersectionObserverAPI;
    using Blazor.Essentials.ResizeObserverAPI;

    using Microsoft.Extensions.DependencyInjection;

    public static class BlazorEssentialsExtensions
    {
        public static IServiceCollection AddBlazorEssentials(this IServiceCollection services)
        {
            return services
                .AddSingleton<IResizeObserverFactory, ResizeObserverFactory>()
                .AddSingleton<IIntersectionObserverFactory, IntersectionObserverFactory>();
        }
    }
}
