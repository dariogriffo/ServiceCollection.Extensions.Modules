namespace ServiceCollection.Extensions.Modules
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class Module
    {
        private static readonly HashSet<Type> LoadedModules = new HashSet<Type>();
        private readonly IServiceCollection _childServices = new ServiceCollection();

        protected internal Module()
        {
        }

        protected virtual void Load(IServiceCollection services)
        {
        }

        internal IServiceCollection Loader<T>(IServiceCollection services, T module)
            where T : Module
        {
            var type = module.GetType();
            if (LoadedModules.Contains(type))
            {
                // Avoid duplicated load
                return services;
            }

            LoadedModules.Add(type);

            Load(_childServices);
            foreach (var sd in _childServices)
            {
                services.Add(sd);
            }

            return services;
        }
    }
}
