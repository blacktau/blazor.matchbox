using Blazor.Matchbox.Common;
using Microsoft.AspNetCore.Components;

namespace Blazor.Matchbox.IntersectionObserverAPI
{
    using Blazor.Matchbox.Common;

    public class IntersectionObserverEntry
    {
        public DomRectReadOnly BoundingClientRect { get; set; }

        public decimal IntersectionRatio { get; set;  }

        public DomRectReadOnly IntersectionRect { get; set;  }

        public bool IsIntersecting { get; set; }

        public DomRectReadOnly RootBounds { get; set;  }

        public ElementReference Target { get; set;  }

        public decimal Time { get; set;  }
    }
}