namespace Blazor.Matchbox.Observers.Intersection
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    public interface IIntersectionObserver : IAsyncDisposable
    {
        ValueTask DisconnectAsync();

        ValueTask ObserveAsync(ElementReference targetElement);

        ValueTask<List<IntersectionObserverEntry>> TakeRecordsAsync();

        ValueTask UnobserveAsync(ElementReference target);
    }
}