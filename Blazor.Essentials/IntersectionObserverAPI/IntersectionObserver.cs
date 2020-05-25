using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Blazor.Essentials.IntersectionObserverAPI
{
    public sealed class IntersectionObserver : IIntersectionObserver, IDisposable
    {
        private readonly IJSRuntime jsRuntime;
        private readonly Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback;
        private readonly ILogger<IntersectionObserver> logger;
        private readonly DotNetObjectReference<IntersectionObserver> reference;
        private readonly string instanceKey;
        private bool disposedValue;

        public ElementReference Root => throw new System.NotImplementedException();

        public string RootMargin => throw new System.NotImplementedException();

        public int[] Thresholds => throw new System.NotImplementedException();

        public IntersectionObserver(IJSRuntime jsRuntime, Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback, ILogger<IntersectionObserver> logger)
        {
            this.jsRuntime = jsRuntime;
            this.callback = callback;
            this.logger = logger;
            this.instanceKey = Guid.NewGuid().ToString();
            this.reference = DotNetObjectReference.Create(this);
            this.jsRuntime.InvokeVoidAsync(MethodNames.CREATE, this.instanceKey, this.reference);
        }

        public void Disconnect() => this.jsRuntime.InvokeVoidAsync(MethodNames.DISCONNECT, this.instanceKey);

        public void Observe(ElementReference targetElement)
        {
            this.jsRuntime.InvokeVoidAsync(MethodNames.OBSERVE, this.instanceKey, targetElement);
        }

        public List<IntersectionObserverEntry> TakeRecords()
        {
            var entriesJson = this.jsRuntime.InvokeAsync<string>(MethodNames.TAKERECORDS, this.instanceKey).GetAwaiter().GetResult();
            return DeserializeEntries(entriesJson);
        }

        public void Unobserve(ElementReference target)
        {
            this.jsRuntime.InvokeVoidAsync(MethodNames.UNOBSERVE, this.instanceKey, target);
        }

        [JSInvokable("InvokeCallback")]
        public void InvokeCallback(string entriesJson)
        {
            var entries = DeserializeEntries(entriesJson);
            this.callback?.Invoke(entries, this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {

                if (disposing)
                {
                    this.reference?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.jsRuntime?.InvokeVoidAsync(MethodNames.DISPOSE, this.instanceKey);
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~IntersectionObserver()
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

        private List<IntersectionObserverEntry> DeserializeEntries(string entriesJson) 
        {
            JsonSerializerOptions options = new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };

            return JsonSerializer.Deserialize<List<IntersectionObserverEntry>>(entriesJson, options);
        }

        private static class MethodNames
        {
            public const string CREATE = "BlazorEssentials.IntersectionObserverManager.create";
            public const string TAKERECORDS = "BlazorEssentials.IntersectionObserverManager.takeRecords";
            public const string DISCONNECT = "BlazorEssentials.IntersectionObserverManager.disconnect";
            public const string OBSERVE = "BlazorEssentials.IntersectionObserverManager.observe";
            public const string UNOBSERVE = "BlazorEssentials.IntersectionObserverManager.unobserve";
            public const string DISPOSE = "BlazorEssentials.IntersectionObserverManager.dispose";
        }
    }
}