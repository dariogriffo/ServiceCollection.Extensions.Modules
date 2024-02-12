using Microsoft.Extensions.DependencyInjection;
using ServiceCollection.Extensions.Modules.UnitTests.Implementations;
using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;

namespace ServiceCollection.Extensions.Modules.UnitTests.Modules;

public class SimpleModule : Module
{
    protected override void Load(IServiceCollection services)
    {
        base.Load(services);
        services.AddSingleton<IService, ServiceImpl2>();
    }
}
