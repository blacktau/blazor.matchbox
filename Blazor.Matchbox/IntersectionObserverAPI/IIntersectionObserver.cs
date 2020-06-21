using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazor.Matchbox.IntersectionObserverAPI
{
    public interface IIntersectionObserver : IAsyncDisposable
    {
        ValueTask DisconnectAsync();

        ValueTask ObserveAsync(ElementReference targetElement);

        ValueTask<List<IntersectionObserverEntry>> TakeRecordsAsync();

        ValueTask UnobserveAsync(ElementReference target);
    }
}