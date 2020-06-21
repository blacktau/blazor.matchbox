namespace Blazor.Matchbox.Tests.IntersectionObserverAPI
{
    using Blazor.Matchbox.IntersectionObserverAPI;

    using Bunit;
    using Bunit.Mocking.JSInterop;

    using Xunit;

    public class IntersectionObserverFactoryTests
    {
        public class Constructor : TestContext
        {
            [Fact]
            public void GivenValidInputConstructs()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                var observerFactory = new IntersectionObserverFactory(jsMock.ToJsRuntime());

                Assert.NotNull(observerFactory);
            }
        }
        
        public class CreateObserver : TestContext
        {
            [Fact]
            public void GivenValidInputConstructsObserver()
            {
                var jsMock = this.Services.AddMockJsRuntime();

                var observerFactory = new IntersectionObserverFactory(jsMock.ToJsRuntime());

                var observer = observerFactory.CreateObserver(
                    ((entries, intersectionObserver) =>
                    {
                    }));

                Assert.NotNull(observer);
            }
        }

    }
}
