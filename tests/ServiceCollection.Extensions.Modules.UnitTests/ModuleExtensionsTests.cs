using ServiceCollection.Extensions.Modules.UnitTests.Implementations;

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
        public void RegisterModule_WhenAFactoryFunctionIsInvoked_AModuleIsRegistered()
        {
            var serviceCollection = new ServiceCollection();
            if (new Random().Next() % 2 == 0)
            {
                serviceCollection.AddSingleton<IService, ServiceImpl2>();
            }
            else
            {
                serviceCollection.AddSingleton<IService, ServiceImpl1>();
            }

            serviceCollection.RegisterModule((collection) =>
            {
                var impl = collection.BuildServiceProvider().GetRequiredService<IService>();
                return new ParameterizedModule(impl);
            });

            using var scope1 = serviceCollection.BuildServiceProvider();
            var service = scope1.GetRequiredService<IService2>();
            service.Message().Should().Be(scope1.GetRequiredService<IService>().Message());
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
            Action act = () => serviceCollection.RegisterModule<TestModule>();
            act.Should().NotThrow();
        }

        [Test]
        public void RegisterModule_WhenCalledTwice_RegistrationHappensOnlyOnce()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterModule<SimpleModule>().RegisterModule<SimpleModule>();
            serviceCollection.Count.Should().Be(1);
        }

        [Test]
        public void RegisterModule_InDifferentCollections_WorksCorrectly()
        {
            var services1 = new ServiceCollection();
            var services2 = new ServiceCollection();
            services1.RegisterModule<SimpleModule>();
            services2.RegisterModule<SimpleModule>();
            services1.Count.Should().Be(1);
            services2.Count.Should().Be(1);
        }
    }
}
