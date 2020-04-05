namespace ServiceCollection.Extensions.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class Module
    {
        private static readonly HashSet<Type> LoadedModules = new HashSet<Type>();

        private readonly IServiceCollection _childServices = new ServiceCollection();

        private IServiceCollection _services;

        protected internal Module()
        {
        }

        protected virtual void Load(IServiceCollection services)
        {

        }

        internal IServiceCollection Loader<T>(IServiceCollection services, T module)
            where T: Module
        {
            var type = module.GetType();
            if (LoadedModules.Contains(type))
            {
                // Avoid duplicated load
                return _services;
            }

            LoadedModules.Add(type);

            _services = services;
            Load(_childServices);
            foreach (var sd in _childServices)
            {
                _services.Add(sd);
            }

            return _services;
        }
    }
}
