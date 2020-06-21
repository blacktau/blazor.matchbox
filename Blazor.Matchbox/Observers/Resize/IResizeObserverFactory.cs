namespace Blazor.Matchbox.Observers.Resize
{
    using System;
    using System.Collections.Generic;

    public interface IResizeObserverFactory
    {
        IResizeObserver CreateObserver(Action<IEnumerable<ResizeObserverEntry>,IResizeObserver> callback);
    }
}
