namespace Blazor.Essentials.ResizeObserverAPI
{
    using System;
    using Microsoft.AspNetCore.Components;

    public interface IResizeObserver : IDisposable
    {
        void Disconnect();

        void Observe(ElementReference reference);

        void Unobserve(ElementReference target);
    }
}
