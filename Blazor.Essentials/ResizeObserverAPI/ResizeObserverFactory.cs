namespace Blazor.Essentials.ResizeObserverAPI
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    internal class ResizeObserverFactory : IResizeObserverFactory
    {
        private readonly IJSRuntime jsRuntime;
        private readonly ILoggerFactory loggerFactory;

        public ResizeObserverFactory(IJSRuntime jsRuntime, ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.jsRuntime = jsRuntime;
        }

        public IResizeObserver CreateObserver(Action<IEnumerable<ResizeObserverEntry>, IResizeObserver> callback)
            => new ResizeObserver(this.jsRuntime, callback, loggerFactory.CreateLogger<ResizeObserver>());
    }
}
