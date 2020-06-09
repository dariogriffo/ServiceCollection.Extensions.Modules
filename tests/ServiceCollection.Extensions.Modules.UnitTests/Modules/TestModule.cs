namespace ServiceCollection.Extensions.Modules.UnitTests.Modules
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class TestModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            base.Load(services);
            var _ = (services.FirstOrDefault(x => x.ServiceType == typeof(ILoggerFactory)) ?? throw new ApplicationException());
        }
    }
}