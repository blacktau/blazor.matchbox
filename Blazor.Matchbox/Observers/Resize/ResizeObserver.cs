namespace Blazor.Matchbox.Observers.Resize
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    public sealed class ResizeObserver : IResizeObserver
    {
        private readonly IJSRuntime jsRuntime;
        private readonly Action<List<ResizeObserverEntry>, IResizeObserver> callback;
        private readonly string instanceKey;
        private readonly DotNetObjectReference<ResizeObserver> reference;
        private bool disposedValue;

        public ResizeObserver(
            IJSRuntime jsRuntime, 
            Action<List<ResizeObserverEntry>, IResizeObserver> callback)
        {
            this.jsRuntime = jsRuntime;
            this.callback = callback;
            this.instanceKey = Guid.NewGuid().ToString();
            this.reference = DotNetObjectReference.Create(this);
            this.Initialize();
        }

        public ValueTask DisconnectAsync() 
        { 
            return this.jsRuntime.InvokeVoidAsync(MethodNames.Disconnect, this.instanceKey);
        }

        public ValueTask ObserveAsync(ElementReference targetElement)
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.Observe, this.instanceKey, targetElement);
        }

        public ValueTask UnobserveAsync(ElementReference target)
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.Unobserve, this.instanceKey, target);
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
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.reference?.Dispose();
                }
                
                this.disposedValue = true;

                if (this.jsRuntime != null) 
                {
                     return this.jsRuntime.InvokeVoidAsync(MethodNames.Dispose, this.instanceKey);
                }
            }

            return new ValueTask();
        }
        
        public async ValueTask DisposeAsync()
        {
            await this.Dispose(disposing: true).ConfigureAwait(false);
        }

        private async void Initialize() {
            await this.jsRuntime.InvokeVoidAsync(MethodNames.Create, this.instanceKey, this.reference).ConfigureAwait(false);
        }

        private static class MethodNames
        {
            public const string Create = "BlazorMatchbox.ResizeObserverManager.create";
            public const string Disconnect = "BlazorMatchbox.ResizeObserverManager.disconnect";
            public const string Observe = "BlazorMatchbox.ResizeObserverManager.observe";
            public const string Unobserve = "BlazorMatchbox.ResizeObserverManager.unobserve";
            public const string Dispose = "BlazorMatchbox.ResizeObserverManager.dispose";
        }
    }
}
