namespace Blazor.Essentials.ResizeObserverAPI.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IResizeObserverFactory
    {
        IResizeObserver CreateResizeObserver(Action<IEnumerable<ResizeObserverEntry>,IResizeObserver> callback);
    }
}
