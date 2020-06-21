using System;
using System.Collections.Generic;

namespace Blazor.Matchbox.IntersectionObserverAPI
{
    public interface IIntersectionObserverFactory
    {
        IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback, IntersectionObserverOptions options);
        
        IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback);
    }
}