namespace ServiceCollection.Extensions.Modules.UnitTests.Modules
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules.UnitTests.Implementations;
    using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;

    public class OuterModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            base.Load(services);
            services.RegisterModule<InnerModule>();
            services.AddSingleton<IService, ServiceImpl3>();
        }
    }
}