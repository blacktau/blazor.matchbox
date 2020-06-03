namespace Blazor.Essentials.ResizeObserverAPI
{
    using System;
    using System.Collections.Generic;

    public interface IResizeObserverFactory
    {
        IResizeObserver CreateObserver(Action<IEnumerable<ResizeObserverEntry>,IResizeObserver> callback);
    }
}
