namespace Blazor.Essentials.ResizeObserverAPI
{
    using System;
    using System.Collections.Generic;

    using Blazor.Essentials.ResizeObserverAPI.Interfaces;
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

        public IResizeObserver CreateResizeObserver(Action<IEnumerable<ResizeObserverEntry>, IResizeObserver> callback)
            => new ResizeObserver(this.jsRuntime, callback, loggerFactory.CreateLogger<ResizeObserver>());
    }
}
