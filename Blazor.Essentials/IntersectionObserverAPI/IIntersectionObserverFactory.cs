using System;
using System.Collections.Generic;

namespace Blazor.Essentials.IntersectionObserverAPI
{
    public interface IIntersectionObserverFactory
    {
        IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback, IntersectionObserverOptions options);
        
        IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback);
    }
}