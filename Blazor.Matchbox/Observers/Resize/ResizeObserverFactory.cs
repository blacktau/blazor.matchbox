namespace Blazor.Matchbox.Observers.Resize
{
    using System;
    using System.Collections.Generic;

    using Microsoft.JSInterop;

    public class ResizeObserverFactory : IResizeObserverFactory
    {
        private readonly IJSRuntime jsRuntime;

        public ResizeObserverFactory(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public IResizeObserver CreateObserver(Action<IEnumerable<ResizeObserverEntry>, IResizeObserver> callback)
            => new ResizeObserver(this.jsRuntime, callback);
    }
}
