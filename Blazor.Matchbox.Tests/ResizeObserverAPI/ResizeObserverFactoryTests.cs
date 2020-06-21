namespace Blazor.Matchbox.Tests.ResizeObserverAPI
{
    using Blazor.Matchbox.ResizeObserverAPI;

    using Bunit;
    using Bunit.Mocking.JSInterop;

    using Xunit;

    public class ResizeObserverFactoryTests
    {
        public class Constructor : TestContext
        {
            [Fact]
            public void GivenValidInputConstructs()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                var observerFactory = new ResizeObserverFactory(jsMock.ToJsRuntime());

                Assert.NotNull(observerFactory);
            }
        }
        
        public class CreateObserver : TestContext
        {
            [Fact]
            public void GivenValidInputConstructsObserver()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                var observerFactory = new ResizeObserverFactory(jsMock.ToJsRuntime());

                var observer = observerFactory.CreateObserver(
                    ((entries, resizeObserver) =>
                    {
                    }));

                Assert.NotNull(observer);
            }
        }
    }
}
