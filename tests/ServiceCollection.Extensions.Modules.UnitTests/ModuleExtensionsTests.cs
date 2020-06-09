namespace ServiceCollection.Extensions.Modules.UnitTests
{
    using System;
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;
    using ServiceCollection.Extensions.Modules.UnitTests.Modules;
    using NUnit.Framework;

    [TestFixture]
    public class ModuleExtensionsTests
    {
        [Test]
        public void RegisterModule_WithNestedModules_AllDependenciesAreRegistered()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterModule<OuterModule>();
            
            using var scope1 = serviceCollection.BuildServiceProvider();
            var services = scope1.GetRequiredService<IEnumerable<IService>>();
            services.Count().Should().Be(2);
        }

        [Test]
        public void RegisterModule_WhenCalled_PreviousRegistrationsArePresent()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(c =>
            {
                c.ClearProviders();
                c.SetMinimumLevel(LogLevel.Debug);
                c.AddNLog();
            });
            Action act = ()=> serviceCollection.RegisterModule<TestModule>();
            act.Should().NotThrow();
        }
    }
}
