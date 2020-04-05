namespace ServiceCollection.Extensions.Modules.UnitTests.Modules
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules.UnitTests.Helpers;
    using ServiceCollection.Extensions.Modules.UnitTests.Implementations;
    using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;

    public class TestModule2 : Module
    {
        private readonly CountHolder _holder;

        internal TestModule2(CountHolder holder)
            :base()
        {
            _holder = holder;
        }

        protected override void Load(IServiceCollection services)
        {
            base.Load(services);
            services.AddSingleton<IService, ServiceImpl2>();
            _holder.Increment();
        }
    }
}
