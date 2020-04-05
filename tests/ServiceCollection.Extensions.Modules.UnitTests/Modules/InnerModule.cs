namespace ServiceCollection.Extensions.Modules.UnitTests.Modules
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules.UnitTests.Implementations;
    using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;

    public class InnerModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            base.Load(services); 
            services.AddSingleton<IService, ServiceImpl2>();
        }
    }
}