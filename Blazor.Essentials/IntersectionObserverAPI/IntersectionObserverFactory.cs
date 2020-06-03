using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Blazor.Essentials.IntersectionObserverAPI
{
    public class IntersectionObserverFactory : IIntersectionObserverFactory
    {
        private readonly IJSRuntime jsRuntime;
        private readonly ILoggerFactory loggerFactory;

        public IntersectionObserverFactory(IJSRuntime jsRuntime, ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.jsRuntime = jsRuntime;
        }

        public IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback) 
        {
            return CreateObserver(callback, null);
        }

        public IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback, IntersectionObserverOptions options)
        {
            return new IntersectionObserver(jsRuntime, callback, options, loggerFactory.CreateLogger<IntersectionObserver>());
        }
    }
}