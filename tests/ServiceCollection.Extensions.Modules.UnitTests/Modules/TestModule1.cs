namespace ServiceCollection.Extensions.Modules.UnitTests.Modules
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules;
    using ServiceCollection.Extensions.Modules.UnitTests.Implementations;
    using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;

    public class TestModule1 : Module
    {
        internal static int LoadedCount = 0;

        protected override void Load(IServiceCollection services)
        {
            base.Load(services);
            services.AddSingleton<IService, ServiceImpl1>();
            ++LoadedCount;
        }
    }
}