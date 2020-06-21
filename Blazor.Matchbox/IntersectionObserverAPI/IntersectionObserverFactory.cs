using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace Blazor.Matchbox.IntersectionObserverAPI
{
    public class IntersectionObserverFactory : IIntersectionObserverFactory
    {
        private readonly IJSRuntime jsRuntime;

        public IntersectionObserverFactory(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback) => 
            this.CreateObserver(callback, null);

        public IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback, IntersectionObserverOptions options) => 
            new IntersectionObserver(jsRuntime, callback, options);
    }
}