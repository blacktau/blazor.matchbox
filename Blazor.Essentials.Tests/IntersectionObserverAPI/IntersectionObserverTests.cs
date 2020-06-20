namespace Blazor.Essentials.Tests.IntersectionObserverAPI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Blazor.Essentials.IntersectionObserverAPI;

    using Bunit;
    using Bunit.Mocking.JSInterop;

    using Microsoft.AspNetCore.Components;

    using Xunit;

    public class IntersectionObserverTests
    {
        private const string Create = "BlazorEssentials.IntersectionObserverManager.create";
        private const string TakeRecords = "BlazorEssentials.IntersectionObserverManager.takeRecords";
        private const string Disconnect = "BlazorEssentials.IntersectionObserverManager.disconnect";
        private const string Observe = "BlazorEssentials.IntersectionObserverManager.observe";
        private const string Unobserve = "BlazorEssentials.IntersectionObserverManager.unobserve";
        private const string Dispose = "BlazorEssentials.IntersectionObserverManager.dispose";
        
        private const string IntersectionResult = 
            "[{\"boundingClientRect\":{\"width\":962,\"height\":352,\"top\":1011.5999755859375,\"right\":1244,\"bottom\":1363.5999755859375,\"left\":282},\"intersectionRect\": {\"width\":1234.5,\"height\":1235.6,\"top\":1236.7,\"right\":1237.8,\"bottom\":1238.9,\"left\":1239.01},\"rootBounds\": {\"width\":1268,\"height\":880,\"top\":1,\"right\":1268,\"bottom\":880,\"left\":2},\"intersectionRatio\":0.234,\"isIntersecting\":true,\"time\":4868,\"target\":{}}]";
        
        public class Constructor : TestContext
        {
            [Fact]
            public void GivenValidInputConstructs()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                static void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // no op
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);
                
                Assert.NotNull(observer);
            }
            
            [Fact]
            public void InvokesCreateOnJsRunTime()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                static void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // no op
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

                jsMock.VerifyInvoke(Create);
            }
            
            [Fact]
            public void InvokedCreateSuppliesNewInstanceKeyEachTime()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                static void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // no op
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);
                var observer2 = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

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

                static void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // no op
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

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

                static void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // no op
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

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

                static void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // no op
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

                var elementRef = new ElementReference();
                
                observer.UnobserveAsync(elementRef);

                jsMock.VerifyInvoke(Unobserve);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disconnectKey = jsMock.Invocations[Unobserve][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disconnectKey);
                Assert.Equal(elementRef, jsMock.Invocations[Unobserve][0].Arguments[1]);
            }
        }
        
        public class TakeRecordsAsync : TestContext
        {
            [Fact]
            public void InvokesTakeRecordsOnJsRuntime()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                jsMock
                    .Setup<string>(TakeRecords)
                    .SetResult(IntersectionResult);

                static void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // no op
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

                observer.TakeRecordsAsync();

                jsMock.VerifyInvoke(TakeRecords);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var takeRecordsKey = jsMock.Invocations[TakeRecords][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, takeRecordsKey);
            }
            
            [Fact]
            public async Task DeserializesCorrectly()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                jsMock
                    .Setup<string>(TakeRecords,list => true)
                    .SetResult(IntersectionResult);

                static void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // no op
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

                var entries = await observer.TakeRecordsAsync();
                
                Assert.Single(entries);
                
                var entry = entries[0];
                
                Assert.NotNull(entry.BoundingClientRect);
                Assert.Equal(962, entry.BoundingClientRect.Width);
                Assert.Equal(352, entry.BoundingClientRect.Height);
                Assert.Equal(1011.5999755859375M, entry.BoundingClientRect.Top);
                Assert.Equal(1244, entry.BoundingClientRect.Right);
                Assert.Equal(1363.5999755859375M, entry.BoundingClientRect.Bottom);
                Assert.Equal(282, entry.BoundingClientRect.Left);

                Assert.NotNull(entry.IntersectionRect);
                Assert.Equal(1234.5M, entry.IntersectionRect.Width);
                Assert.Equal(1235.6M, entry.IntersectionRect.Height);
                Assert.Equal(1236.7M, entry.IntersectionRect.Top);
                Assert.Equal(1237.8M, entry.IntersectionRect.Right);
                Assert.Equal(1238.9M, entry.IntersectionRect.Bottom);
                Assert.Equal(1239.01M, entry.IntersectionRect.Left);
                
                Assert.NotNull(entry.RootBounds);
                Assert.Equal(1268, entry.RootBounds.Width);
                Assert.Equal(880, entry.RootBounds.Height);
                Assert.Equal(1, entry.RootBounds.Top);
                Assert.Equal(1268, entry.RootBounds.Right);
                Assert.Equal(880, entry.RootBounds.Bottom);
                Assert.Equal(2, entry.RootBounds.Left);
                
                Assert.Equal(0.234M, entry.IntersectionRatio);
                Assert.True(entry.IsIntersecting);
                Assert.Equal(4868, entry.Time);
                
                Assert.Equal(default(ElementReference), entry.Target);
            }

            private bool ArumentsMatcher(IReadOnlyList<object> arg) => true;
        }
        
        public class InvokeCallback : TestContext
        {
            [Fact]
            public void WhenInvokedInvokesCallback()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                bool invoked = false;
                
                void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    invoked = true;
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

                observer.InvokeCallback(IntersectionResult);

                Assert.True(invoked);
            }
        }
        
        public class DisposeAsync : TestContext
        {
            [Fact]
            public void WhenInvokedInvokesDisposeOnJsRuntime()
            {
                var jsMock = this.Services.AddMockJsRuntime();
              
                void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // noop
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

                observer.DisposeAsync();

                jsMock.VerifyInvoke(IntersectionObserverTests.Dispose);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disposeKey = jsMock.Invocations[IntersectionObserverTests.Dispose][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disposeKey);
            }
            
            [Fact]
            public void WhenInvokedTwiceCallsDisposeOnJsRuntimeOnce()
            {
                var jsMock = this.Services.AddMockJsRuntime();
              
                void Callback(List<IntersectionObserverEntry> list, IIntersectionObserver intersectionObserver)
                {
                    // noop
                }

                var options = new IntersectionObserverOptions();
                
                var observer = new IntersectionObserver(jsMock.ToJsRuntime(), Callback, options);

                observer.DisposeAsync();
                observer.DisposeAsync();

                jsMock.VerifyInvoke(IntersectionObserverTests.Dispose);
                
                var instanceKey = jsMock.Invocations[Create][0].Arguments[0];
                var disposeKey = jsMock.Invocations[IntersectionObserverTests.Dispose][0].Arguments[0]; 
                
                Assert.Equal(instanceKey, disposeKey);
            }
        }

    }
}
