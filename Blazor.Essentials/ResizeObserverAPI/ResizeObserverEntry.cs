namespace Blazor.Essentials.ResizeObserverAPI.Interfaces
{
    using Blazor.Essentials.Common;

    using Microsoft.AspNetCore.Components;

    public class ResizeObserverEntry
    {
        public BoxSize BorderBoxSize { get; set; }
        
        public BoxSize ContentBoxSize { get; set; }
        
        public DomRectReadOnly ContentRect { get; set; }
        
        public ElementReference Target { get; set; }
    }
}
