namespace Blazor.Matchbox.Observers.Intersection
{
    using System;
    using System.Collections.Generic;

    public interface IIntersectionObserverFactory
    {
        IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback, IntersectionObserverOptions options);
        
        IIntersectionObserver CreateObserver(Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback);
    }
}