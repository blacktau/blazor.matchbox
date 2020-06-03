using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Blazor.Essentials.IntersectionObserverAPI
{
    public interface IIntersectionObserver : IDisposable
    {
        void Disconnect();

        void Observe(ElementReference targetElement);

        List<IntersectionObserverEntry> TakeRecords();

        void Unobserve(ElementReference target);
    }
}