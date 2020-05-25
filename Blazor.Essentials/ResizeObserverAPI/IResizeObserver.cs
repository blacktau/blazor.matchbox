namespace Blazor.Essentials.ResizeObserverAPI.Interfaces
{
    using Microsoft.AspNetCore.Components;

    public interface IResizeObserver
    {
        void Disconnect();

        void Observe(ElementReference reference, ResizeObserverOptions options = null);

        void Unobserve(ElementReference target);
    }
}