namespace Blazor.Matchbox.Observers.Resize
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    public interface IResizeObserver : IAsyncDisposable
    {
        ValueTask DisconnectAsync();

        ValueTask ObserveAsync(ElementReference reference);

        ValueTask UnobserveAsync(ElementReference target);
    }
}
