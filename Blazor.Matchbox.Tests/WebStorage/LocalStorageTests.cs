namespace Blazor.Matchbox.Tests.WebStorage
{
    using System;
    using System.Threading.Tasks;

    using Blazor.Matchbox.WebStorage;

    using Bunit;
    using Bunit.Mocking.JSInterop;

    using Xunit;

    public class LocalStorageTests
    {
        private const string Length = "BlazorMatchbox.LocalStorageProxy.getLength";
        private const string Key = "BlazorMatchbox.LocalStorageProxy.key";
        private const string GetItem = "BlazorMatchbox.LocalStorageProxy.getItem";
        private const string SetItem = "BlazorMatchbox.LocalStorageProxy.setItem";
        private const string RemoveItem = "BlazorMatchbox.LocalStorageProxy.removeItem";
        private const string Clear = "BlazorMatchbox.LocalStorageProxy.clear";
        
        public class Constructor : TestContext
        {
            [Fact]
            public void GivenValidInputsConstructs()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                var localStorage = new LocalStorage(jsMock.ToJsRuntime());
                
                Assert.NotNull(localStorage);
            }
        }
        
        public class GetLengthAsync : TestContext
        {
            [Fact]
            public async Task InvokesGetLengthInJavascript()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                jsMock
                    .Setup<int>(Length)
                    .SetResult(12345);
                    
                var localStorage = new LocalStorage(jsMock.ToJsRuntime());

                var result = await localStorage.GetLengthAsync();
                
                jsMock.VerifyInvoke(Length);
                Assert.Equal(12345, result);
            }
        }
        
        public class KeyAsync : TestContext
        {
            private const string TheKey = "TheKey";
            
            [Fact]
            public async Task InvokesKeyInJavascript()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                jsMock
                    .Setup<string>(Key, list => Convert.ToInt32(list[0]) == 12345)
                    .SetResult(TheKey);
                    
                var localStorage = new LocalStorage(jsMock.ToJsRuntime());

                var result = await localStorage.KeyAsync(12345);
                
                jsMock.VerifyInvoke(Key);
                Assert.Equal(TheKey, result);
            }
        }
        
        public class GetItemAsync : TestContext
        {
            private const string TheKey = "TheKey";
            private const string TheValue = "TheValue";
            
            [Fact]
            public async Task InvokesGetItemInJavascript()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                jsMock
                    .Setup<string>(GetItem, list => Convert.ToString(list[0]) == TheKey)
                    .SetResult(TheValue);
                    
                var localStorage = new LocalStorage(jsMock.ToJsRuntime());

                var result = await localStorage.GetItemAsync(TheKey);
                
                Assert.NotNull(localStorage);
                jsMock.VerifyInvoke(GetItem);
                Assert.Equal(TheValue, result);
            }
        }
        
        public class SetItemAsync : TestContext
        {
            private const string TheKey = "TheKey";
            private const string TheValue = "TheValue";
            
            [Fact]
            public async Task InvokesSetItemInJavascript()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                jsMock
                    .SetupVoid(SetItem);
                    
                var localStorage = new LocalStorage(jsMock.ToJsRuntime());

                await localStorage.SetItemAsync(TheKey, TheValue);
                
                Assert.NotNull(localStorage);
                jsMock.VerifyInvoke(SetItem);
                Assert.Equal(TheKey, jsMock.Invocations[SetItem][0].Arguments[0]);
                Assert.Equal(TheValue, jsMock.Invocations[SetItem][0].Arguments[1]);
            }
        }
        
        public class RemoveItemAsync : TestContext
        {
            private const string TheKey = "TheKey";
            
            [Fact]
            public async Task InvokesRemoveItemInJavascript()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                jsMock
                    .SetupVoid(RemoveItem);
                    
                var localStorage = new LocalStorage(jsMock.ToJsRuntime());

                await localStorage.RemoveItemAsync(TheKey);
                
                Assert.NotNull(localStorage);
                jsMock.VerifyInvoke(RemoveItem);
                Assert.Equal(TheKey, jsMock.Invocations[RemoveItem][0].Arguments[0]);
            }
        }
        
        public class ClearAsync : TestContext
        {
           
            [Fact]
            public async Task InvokesClearInJavascript()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                    
                var localStorage = new LocalStorage(jsMock.ToJsRuntime());

                await localStorage.ClearAsync();
                
                Assert.NotNull(localStorage);
                jsMock.VerifyInvoke(Clear);
            }
        }
    }
}
