namespace Blazor.Essentials.Tests.ResizeObserverAPI
{
    using Blazor.Essentials.ResizeObserverAPI;

    using Bunit;
    using Bunit.Mocking.JSInterop;

    using Microsoft.Extensions.Logging;
    
    using Moq;

    using Xunit;

    public class ResizeObserverFactoryTests
    {
        public class Constructor : TestContext
        {
            [Fact]
            public void GivenValidInputConstructs()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILoggerFactory>();

                var observerFactory = new ResizeObserverFactory(jsMock.ToJsRuntime(), logger);

                Assert.NotNull(observerFactory);
            }
        }
        
        public class CreateObserver : TestContext
        {
            [Fact]
            public void GivenValidInputConstructsObserver()
            {
                var jsMock = this.Services.AddMockJsRuntime();
                var logger = Mock.Of<ILoggerFactory>();

                var observerFactory = new ResizeObserverFactory(jsMock.ToJsRuntime(), logger);

                var observer = observerFactory.CreateObserver(
                    ((entries, resizeObserver) =>
                    {
                    }));

                Assert.NotNull(observer);
            }
        }
    }
}
