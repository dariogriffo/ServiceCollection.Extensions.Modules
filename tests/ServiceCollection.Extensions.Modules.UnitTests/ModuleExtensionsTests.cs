namespace ServiceCollection.Extensions.Modules.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules.UnitTests.Helpers;
    using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;
    using ServiceCollection.Extensions.Modules.UnitTests.Modules;
    using NUnit.Framework;

    [TestFixture]
    public class ModuleExtensionsTests
    {
        [Test]
        public void RegisterModule_WithGeneric_DuplicatesOnlyLoadsOnce()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterModule<TestModule1>();
            serviceCollection.RegisterModule<TestModule1>();

            using var scope1 = serviceCollection.BuildServiceProvider();
            scope1
                .GetRequiredService<IService>()
                .Message()
                .Should().Be("1");

            using var scope2 = serviceCollection.BuildServiceProvider();
            scope2
                .GetRequiredService<IService>()
                .Message()
                .Should().Be("1");

            TestModule1.LoadedCount.Should().Be(1);
        }

        [Test]
        public void RegisterModule_WithInstance_DuplicatesOnlyLoadsOnce()
        {
            var counter = new CountHolder();
            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterModule(new TestModule2(counter));
            serviceCollection.RegisterModule(new TestModule2(counter));

            using var scope1 = serviceCollection.BuildServiceProvider();
            scope1
                .GetRequiredService<IService>()
                .Message()
                .Should().Be("2");

            using var scope2 = serviceCollection.BuildServiceProvider();
            scope2
                .GetRequiredService<IService>()
                .Message()
                .Should().Be("2");

            counter.Count.Should().Be(1);
        }

        [Test]
        public void RegisterModule_WithNestedModules_AllDependenciesAreRegistered()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterModule<OuterModule>();
            
            using var scope1 = serviceCollection.BuildServiceProvider();
            var services = scope1.GetRequiredService<IEnumerable<IService>>();
            services.Count().Should().Be(2);
        }
    }
}
