using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Blazor.Essentials.IntersectionObserverAPI
{
    public interface IIntersectionObserver
    {
        ElementReference Root { get; }

        string RootMargin { get; }   

        int[] Thresholds { get; }

        void Disconnect();

        void Observe(ElementReference targetElement);

        List<IntersectionObserverEntry> TakeRecords();

        void Unobserve(ElementReference target);
    }
}