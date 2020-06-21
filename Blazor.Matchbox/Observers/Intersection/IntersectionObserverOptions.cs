namespace Blazor.Matchbox.Observers.Intersection
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Components;

    public class IntersectionObserverOptions
    {
        public ElementReference? Root { get; set; }

        public string RootMargin { get; set; }

        public List<decimal> Threshold { get; private set; }

        public IntersectionObserverOptions()
        {
            Threshold = new List<decimal>();
        }
    }
}