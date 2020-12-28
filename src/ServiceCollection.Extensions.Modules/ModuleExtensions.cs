using System;

namespace ServiceCollection.Extensions.Modules
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ModuleExtensions
    {
        public static IServiceCollection RegisterModule<T>(this IServiceCollection services, T module)
            where T : Module
        {
            return module.Loader<T>(services);
        }

        public static IServiceCollection RegisterModule<T>(this IServiceCollection services)
	        where T : Module, new()
        {
	        var module = new T();
	        return module.Loader<T>(services);
        }

        public static IServiceCollection RegisterModule<T>(this IServiceCollection services, Func<IServiceCollection,T> factory)
	        where T : Module
        {
	        var module = factory(services);
	        return module.Loader<T>(services);
        }
    }
}
