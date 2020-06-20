namespace Blazor.Essentials.Tests.ResizeObserverAPI
{
    using System.Collections.Generic;

    using Blazor.Essentials.IntersectionObserverAPI;
    using Blazor.Essentials.ResizeObserverAPI;

    using Bunit;
    using Bunit.Mocking.JSInterop;

    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class ResizeObserverTests
    {
        private const string Create = "BlazorEssentials.ResizeObserverManager.create";
        private const string Disconnect = "BlazorEssentials.ResizeObserverManager.disconnect";
        private const string Observe = "BlazorEssentials.ResizeObserverManager.observe";
        private const string Unobserve = "BlazorEssentials.ResizeObserverManager.unobserve";
        private const string Dispose = "BlazorEssentials.ResizeObserverManager.dispose";

        public class Constructor : TestContext
        {
            [Fact]
            public void GivenValidInputConstructs()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILogger<ResizeObserver>>();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);
                
                Assert.NotNull(observer);
            }
            
            [Fact]
            public void InvokesCreateOnJsRunTime()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILogger<ResizeObserver>>();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);

                jsMock.VerifyInvoke(Create);
            }
            
            [Fact]
            public void InvokedCreateSuppliesNewInstanceKeyEachTime()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILogger<ResizeObserver>>();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);
                var observer2 = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);

                jsMock.VerifyInvoke(Create, 2);
                var key1 = jsMock.Invocations[Create][0].Arguments[0];
                var key2 = jsMock.Invocations[Create][1].Arguments[0];
                
                Assert.NotEqual(key1, key2);
            }
        }
        
        public class DisconnectAsync : TestContext
        {
            [Fact]
            public void InvokesDisconnectOnJsRuntime()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILogger<ResizeObserver>>();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);

                observer.DisconnectAsync();

                jsMock.VerifyInvoke(Disconnect);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disconnectKey = jsMock.Invocations[Disconnect][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disconnectKey);
            }
        }

        public class ObserveAsync : TestContext
        {
            [Fact]
            public void InvokesObserveOnJsRuntime()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILogger<ResizeObserver>>();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);


                var elementRef = new ElementReference();
                
                observer.ObserveAsync(elementRef);

                jsMock.VerifyInvoke(Observe);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disconnectKey = jsMock.Invocations[Observe][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disconnectKey);
                Assert.Equal(elementRef, jsMock.Invocations[Observe][0].Arguments[1]);
            }
        }
        
        public class UnobserveAsync : TestContext
        {
            [Fact]
            public void InvokesUnobserveAsyncOnJsRuntime()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILogger<ResizeObserver>>();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);

                var elementRef = new ElementReference();
                
                observer.UnobserveAsync(elementRef);

                jsMock.VerifyInvoke(Unobserve);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disconnectKey = jsMock.Invocations[Unobserve][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disconnectKey);
                Assert.Equal(elementRef, jsMock.Invocations[Unobserve][0].Arguments[1]);
            }
        }
        
        public class DisposeAsync : TestContext
        {
            [Fact]
            public void WhenInvokedInvokesDisposeOnJsRuntime()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILogger<ResizeObserver>>();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);

                observer.DisposeAsync();

                jsMock.VerifyInvoke(ResizeObserverTests.Dispose);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disposeKey = jsMock.Invocations[ResizeObserverTests.Dispose][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disposeKey);
            }
            
            [Fact]
            public void WhenInvokedTwiceCallsDisposeOnJsRuntimeOnce()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILogger<ResizeObserver>>();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback, logger);

                observer.DisposeAsync();
                observer.DisposeAsync();

                jsMock.VerifyInvoke(ResizeObserverTests.Dispose);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disposeKey = jsMock.Invocations[ResizeObserverTests.Dispose][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disposeKey);
            }
        }


    }
}
