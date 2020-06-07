using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Blazor.Essentials.IntersectionObserverAPI
{
    public sealed class IntersectionObserver : IIntersectionObserver
    {
        private readonly IJSRuntime jsRuntime;
        private readonly Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback;
        private readonly ILogger<IntersectionObserver> logger;
        private readonly DotNetObjectReference<IntersectionObserver> reference;
        private readonly string instanceKey;
        private bool disposedValue;

        public IntersectionObserver(
            IJSRuntime jsRuntime, 
            Action<List<IntersectionObserverEntry>, IIntersectionObserver> callback, 
            IntersectionObserverOptions options, 
            ILogger<IntersectionObserver> logger)
        {
            this.jsRuntime = jsRuntime;
            this.callback = callback;
            this.logger = logger;
            this.instanceKey = Guid.NewGuid().ToString();
            this.reference = DotNetObjectReference.Create(this);
            this.Initialize(options);
        }

        public ValueTask DisconnectAsync()
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.DISCONNECT, this.instanceKey);
        } 

        public ValueTask ObserveAsync(ElementReference targetElement)
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.OBSERVE, this.instanceKey, targetElement);
        }

        public async ValueTask<List<IntersectionObserverEntry>> TakeRecordsAsync()
        {
            var entriesJson = await this.jsRuntime.InvokeAsync<string>(MethodNames.TAKERECORDS, this.instanceKey).ConfigureAwait(false);

            return DeserializeEntries(entriesJson);
        }

        public ValueTask UnobserveAsync(ElementReference target)
        {
            return jsRuntime.InvokeVoidAsync(MethodNames.UNOBSERVE, instanceKey, target);
        }

        [JSInvokable("InvokeCallback")]
        public void InvokeCallback(string entriesJson)
        {
            var entries = DeserializeEntries(entriesJson);
            this.callback?.Invoke(entries, this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(disposing: true).ConfigureAwait(false);
        }

        private async Task DisposeAsync(bool disposing)
        {
            if (!disposedValue)
            {

                if (disposing)
                {
                    this.reference?.Dispose();
                }

                if (this.jsRuntime != null) {
                    await this.jsRuntime.InvokeVoidAsync(MethodNames.DISPOSE, this.instanceKey).ConfigureAwait(false);
                }
                
                disposedValue = true;
            }
        }

        private static List<IntersectionObserverEntry> DeserializeEntries(string entriesJson) 
        {
            JsonSerializerOptions options = new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };

            return JsonSerializer.Deserialize<List<IntersectionObserverEntry>>(entriesJson, options);
        }

        private async void Initialize(IntersectionObserverOptions options) {
            await this.jsRuntime.InvokeVoidAsync(MethodNames.CREATE, this.instanceKey, this.reference, options).ConfigureAwait(false);
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