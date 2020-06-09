namespace ServiceCollection.Extensions.Modules.UnitTests
{
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
    }
}
