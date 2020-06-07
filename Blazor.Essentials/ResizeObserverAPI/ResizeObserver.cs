using System.Threading.Tasks;
namespace Blazor.Essentials.ResizeObserverAPI
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    public sealed class ResizeObserver : IResizeObserver
    {
        private readonly IJSRuntime jsRuntime;
        private readonly Action<List<ResizeObserverEntry>, IResizeObserver> callback;
        private readonly ILogger<ResizeObserver> logger;
        private readonly string instanceKey;
        private readonly DotNetObjectReference<ResizeObserver> reference;
        private bool disposedValue;

        public ResizeObserver(
            IJSRuntime jsRuntime, 
            Action<List<ResizeObserverEntry>, IResizeObserver> callback, 
            ILogger<ResizeObserver> logger)
        {
            this.jsRuntime = jsRuntime;
            this.callback = callback;
            this.logger = logger;
            this.instanceKey = Guid.NewGuid().ToString();
            this.reference = DotNetObjectReference.Create(this);
            this.Initialize();
        }

        public ValueTask DisconnectAsync() 
        { 
            return this.jsRuntime.InvokeVoidAsync(MethodNames.DISCONNECT, this.instanceKey);
        }

        public ValueTask ObserveAsync(ElementReference targetElement)
        {
            logger.LogDebug($"Observe({targetElement})");
            return this.jsRuntime.InvokeVoidAsync(MethodNames.OBSERVE, this.instanceKey, targetElement);
        }

        public ValueTask UnobserveAsync(ElementReference target)
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.UNOBSERVE, this.instanceKey, target);
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
       
        private ValueTask Dispose(bool disposing)
        {
            if (!disposedValue)
            {

                if (disposing)
                {
                    this.reference?.Dispose();
                }
                
                disposedValue = true;

                if (this.jsRuntime != null) 
                {
                     return this.jsRuntime.InvokeVoidAsync(MethodNames.DISCONNECT, this.instanceKey);
                }
            }

            return new ValueTask();
        }
        
        public async ValueTask DisposeAsync()
        {
            await Dispose(disposing: true).ConfigureAwait(false);
        }

        private async void Initialize() {
            await this.jsRuntime.InvokeVoidAsync(MethodNames.CREATE, this.instanceKey, this.reference).ConfigureAwait(false);
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
