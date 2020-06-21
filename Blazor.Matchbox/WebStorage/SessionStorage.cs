using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Blazor.Matchbox.WebStorage
{
    public class SessionStorage : ISessionStorage
    {
        private readonly IJSRuntime jsRuntime;

        public SessionStorage(IJSRuntime jsRuntime) 
        {
            this.jsRuntime = jsRuntime;
        }

        public ValueTask<int> GetLengthAsync() 
        {
            return this.jsRuntime.InvokeAsync<int>(MethodNames.Length);
        }

        public ValueTask<string> KeyAsync(int n)
        {
            return this.jsRuntime.InvokeAsync<string>(MethodNames.Key, n);
        }

        public ValueTask<string> GetItemAsync(string key) 
        {
            return this.jsRuntime.InvokeAsync<string>(MethodNames.GetItem, key);
        }

        public ValueTask SetItemAsync(string key, string value) 
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.SetItem, key, value);
        }

        public ValueTask RemoveItemAsync(string key)
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.RemoveItem, key);
        }

        public ValueTask ClearAsync()
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.Clear);
        }

        private static class MethodNames
        {
            public const string Length = "BlazorMatchbox.SessionStorageProxy.getLength";
            public const string Key = "BlazorMatchbox.SessionStorageProxy.key";
            public const string GetItem = "BlazorMatchbox.SessionStorageProxy.getItem";
            public const string SetItem = "BlazorMatchbox.SessionStorageProxy.setItem";
            public const string RemoveItem = "BlazorMatchbox.SessionStorageProxy.removeItem";
            public const string Clear = "BlazorMatchbox.SessionStorageProxy.clear";
        }
    }
}