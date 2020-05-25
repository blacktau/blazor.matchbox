using Blazor.Essentials.Common;
using Microsoft.AspNetCore.Components;

namespace Blazor.Essentials.IntersectionObserverAPI
{
    public class IntersectionObserverEntry
    {
        public DomRectReadOnly BoundingClientRect { get; }

        public decimal IntersectionRatio { get; }

        public DomRectReadOnly IntersectionRect { get; }

        public bool IsIntersecting { get; set; }

        public DomRectReadOnly RootBounds { get; }

        public ElementReference Target { get; }

        public decimal Time { get; }
    }
}