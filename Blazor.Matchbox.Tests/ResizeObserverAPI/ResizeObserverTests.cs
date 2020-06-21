namespace Blazor.Matchbox.Tests.ResizeObserverAPI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Blazor.Matchbox.ResizeObserverAPI;

    using Bunit;
    using Bunit.Mocking.JSInterop;

    using Microsoft.AspNetCore.Components;

    using Xunit;

    public class ResizeObserverTests
    {
        private const string Create = "BlazorMatchbox.ResizeObserverManager.create";
        private const string Disconnect = "BlazorMatchbox.ResizeObserverManager.disconnect";
        private const string Observe = "BlazorMatchbox.ResizeObserverManager.observe";
        private const string Unobserve = "BlazorMatchbox.ResizeObserverManager.unobserve";
        private const string Dispose = "BlazorMatchbox.ResizeObserverManager.dispose";

        private const string ResizeObserverResult = "[{\"borderBoxSize\":{\"blockSize\":114.7,\"inlineSize\":306.5},\"contentBoxSize\":{\"blockSize\":112.1,\"inlineSize\":304.2},\"contentRect\":{\"x\":1234.56,\"y\":5678.91,\"width\":304.34,\"height\":112.56,\"top\":1234.78,\"right\":304.5,\"bottom\":112.6,\"left\":89.0},\"target\":{}}]";

        public class Constructor : TestContext
        {
            [Fact]
            public void GivenValidInputConstructs()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);
                
                Assert.NotNull(observer);
            }
            
            [Fact]
            public void InvokesCreateOnJsRunTime()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);

                jsMock.VerifyInvoke(Create);
                Assert.NotNull(observer);
            }
            
            [Fact]
            public void InvokedCreateSuppliesNewInstanceKeyEachTime()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);
                var observer2 = new ResizeObserver(jsMock.ToJsRuntime(), Callback);

                jsMock.VerifyInvoke(Create, 2);
                var key1 = jsMock.Invocations[Create][0].Arguments[0];
                var key2 = jsMock.Invocations[Create][1].Arguments[0];
                
                Assert.NotEqual(key1, key2);
                Assert.NotNull(observer);
                Assert.NotNull(observer2);
            }
        }
        
        public class DisconnectAsync : TestContext
        {
            [Fact]
            public void InvokesDisconnectOnJsRuntime()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);

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

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);


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

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);

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

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);

                observer.DisposeAsync();

                jsMock.VerifyInvoke(ResizeObserverTests.Dispose);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disposeKey = jsMock.Invocations[ResizeObserverTests.Dispose][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disposeKey);
            }
            
            [Fact]
            public async Task WhenInvokedTwiceCallsDisposeOnJsRuntimeOnce()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                static void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    // no op
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);

                await observer.DisposeAsync();
                await observer.DisposeAsync();

                jsMock.VerifyInvoke(ResizeObserverTests.Dispose);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disposeKey = jsMock.Invocations[ResizeObserverTests.Dispose][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disposeKey);
            }
        }

        public class InvokeCallback : TestContext
        {
            [Fact]
            public void WhenInvokedInvokesCallback()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                var invoked = false;
                
                void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    invoked = true;
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);

                observer.InvokeCallback(ResizeObserverResult);

                Assert.True(invoked);
            }
            
            [Fact]
            public void WhenInvokedDeserializesCorrectly()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                List<ResizeObserverEntry> entries = null;
                
                void Callback(List<ResizeObserverEntry> list, IResizeObserver resizeObserver)
                {
                    entries = list;
                }

                var observer = new ResizeObserver(jsMock.ToJsRuntime(), Callback);

                observer.InvokeCallback(ResizeObserverResult);

                Assert.NotNull(entries);
                Assert.Single(entries);
                var entry = entries[0];
                
                Assert.NotNull(entry.BorderBoxSize);
                Assert.Equal(114.7M, entry.BorderBoxSize.BlockSize);
                Assert.Equal(306.5M, entry.BorderBoxSize.InlineSize);
                
                Assert.NotNull(entry.ContentBoxSize);
                Assert.Equal(112.1M, entry.ContentBoxSize.BlockSize);
                Assert.Equal(304.2M, entry.ContentBoxSize.InlineSize);
                
                Assert.NotNull(entry.ContentRect);
                Assert.Equal(1234.56M, entry.ContentRect.X);
                Assert.Equal(5678.91M, entry.ContentRect.Y);
                Assert.Equal(304.34M, entry.ContentRect.Width);
                Assert.Equal(112.56M, entry.ContentRect.Height);
                Assert.Equal(1234.78M, entry.ContentRect.Top);
                Assert.Equal(304.5M, entry.ContentRect.Right);
                Assert.Equal(112.6M, entry.ContentRect.Bottom);
                Assert.Equal(89M, entry.ContentRect.Left);
                
                Assert.Equal(default, entry.Target);
            }
        }
    }
}
