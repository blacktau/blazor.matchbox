namespace Blazor.Essentials.ResizeObserverAPI
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    
    using Blazor.Essentials.ResizeObserverAPI.Interfaces;

    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    internal class ResizeObserver : IResizeObserver, IDisposable
    {
        private readonly IJSRuntime jsRuntime;
        private readonly Action<List<ResizeObserverEntry>, IResizeObserver> callback;
        private readonly ILogger<ResizeObserver> logger;
        private readonly string instanceKey;
        private readonly DotNetObjectReference<ResizeObserver> reference;
        private bool disposedValue;

        public ResizeObserver(IJSRuntime jsRuntime, Action<List<ResizeObserverEntry>, IResizeObserver> callback, ILogger<ResizeObserver> logger)
        {
            this.jsRuntime = jsRuntime;
            this.callback = callback;
            this.logger = logger;
            this.instanceKey = Guid.NewGuid().ToString();
            this.reference = DotNetObjectReference.Create(this);
            this.jsRuntime.InvokeVoidAsync(MethodNames.CREATE, this.instanceKey, this.reference);
        }

        public void Disconnect() => this.jsRuntime.InvokeVoidAsync(MethodNames.DISCONNECT, this.instanceKey);

        public void Observe(ElementReference targetElement, ResizeObserverOptions options = null)
        {
            logger.LogDebug($"Observe({targetElement}, {options})");
            this.jsRuntime.InvokeVoidAsync(MethodNames.OBSERVE, this.instanceKey, targetElement, options);
        }

        public void Unobserve(ElementReference target)
        {
            this.jsRuntime.InvokeVoidAsync(MethodNames.UNOBSERVE, this.instanceKey, target);
        }

        [JSInvokable("InvokeCallback")]
        public void InvokeCallback(string entriesJson)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };

            var entries = JsonSerializer.Deserialize<List<ResizeObserverEntry>>(entriesJson, options);
            this.callback?.Invoke(entries, this);
        }
       
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {

                if (disposing)
                {
                    this.reference?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.jsRuntime?.InvokeVoidAsync(MethodNames.DISCONNECT, this.instanceKey);
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~ResizeObserver()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private static class MethodNames
        {
            public const string CREATE = "BlazorEssentials.ResizeObserverManager.create";
            public const string DISCONNECT = "BlazorEssentials.ResizeObserverManager.disconnect";
            public const string OBSERVE = "BlazorEssentials.ResizeObserverManager.observe";
            public const string UNOBSERVE = "BlazorEssentials.ResizeObserverManager.unobserve";
            public const string DISPOSE = "BlazorEssentials.ResizeObserverManager.dispose";
        }
    }
}
