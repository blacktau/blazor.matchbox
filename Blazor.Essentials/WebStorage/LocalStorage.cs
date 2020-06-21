using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Blazor.Essentials.WebStorage
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IJSRuntime jsRuntime;

        public LocalStorage(IJSRuntime jsRuntime) 
        {
            this.jsRuntime = jsRuntime;
        }

        public ValueTask<int> GetLengthAsync() 
        {
            return this.jsRuntime.InvokeAsync<int>(MethodNames.Length);
        }

        public ValueTask<string> KeyAsync(int index)
        {
            return this.jsRuntime.InvokeAsync<string>(MethodNames.Key, index);
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
            public const string Length = "BlazorEssentials.LocalStorageProxy.getLength";
            public const string Key = "BlazorEssentials.LocalStorageProxy.key";
            public const string GetItem = "BlazorEssentials.LocalStorageProxy.getItem";
            public const string SetItem = "BlazorEssentials.LocalStorageProxy.setItem";
            public const string RemoveItem = "BlazorEssentials.LocalStorageProxy.removeItem";
            public const string Clear = "BlazorEssentials.LocalStorageProxy.clear";
        }
    }
}