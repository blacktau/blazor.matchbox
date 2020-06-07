using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Blazor.Essentials.WebStorage
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
            return this.jsRuntime.InvokeAsync<int>(MethodNames.LENGTH);
        }

        public ValueTask<string> KeyAsync(int n)
        {
            return this.jsRuntime.InvokeAsync<string>(MethodNames.KEY, n);
        }

        public ValueTask<string> GetItemAsync(string key) 
        {
            return this.jsRuntime.InvokeAsync<string>(MethodNames.GET_ITEM, key);
        }

        public ValueTask SetItemAsync(string key, string value) 
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.SET_ITEM, key, value);
        }

        public ValueTask RemoveItemAsync(string key)
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.REMOVE_ITEM, key);
        }

        public ValueTask ClearAsync()
        {
            return this.jsRuntime.InvokeVoidAsync(MethodNames.CLEAR);
        }

        private static class MethodNames
        {
            public const string LENGTH = "BlazorEssentials.SessionStorageProxy.getLength";

            public const string KEY = "BlazorEssentials.SessionStorageProxy.key";

            public const string GET_ITEM = "BlazorEssentials.SessionStorageProxy.getItem";

            public const string SET_ITEM = "BlazorEssentials.SessionStorageProxy.setItem";

            public const string REMOVE_ITEM = "BlazorEssentials.SessionStorageProxy.removeItem";

            public const string CLEAR = "BlazorEssentials.SessionStorageProxy.clear";
        }

    }
}