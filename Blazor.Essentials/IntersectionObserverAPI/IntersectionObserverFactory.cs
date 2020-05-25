using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Blazor.Essentials.IntersectionObserverAPI
{
    internal class IntersectionObserverFactory
    {
        private readonly IJSRuntime jsRuntime;
        private readonly ILoggerFactory loggerFactory;

        public IntersectionObserverFactory(IJSRuntime jsRuntime, ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.jsRuntime = jsRuntime;
        }

        public IIntersectionObserver CreateIntersectionObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback) 
        {
            return new IntersectionObserver(jsRuntime, callback, loggerFactory.CreateLogger<IntersectionObserver>());
        }
    }
}