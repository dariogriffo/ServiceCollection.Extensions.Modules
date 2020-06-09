namespace ServiceCollection.Extensions.Modules
{
    using Microsoft.Extensions.DependencyInjection;

    public abstract class Module
    {
        protected internal Module()
        {
        }

        protected virtual void Load(IServiceCollection services)
        {
        }

        internal IServiceCollection Loader<T>(IServiceCollection services, T module)
            where T : Module
        {
            Load(services);
            return services;
        }
    }
}
