using System;
using System.Collections.Generic;

namespace ServiceCollection.Extensions.Modules
{
    using Microsoft.Extensions.DependencyInjection;

    public abstract class Module
    {
        private static HashSet<Type> _loaded = new HashSet<Type>();
        
        protected internal Module()
        {
        }

        protected virtual void Load(IServiceCollection services)
        {
        }

        internal IServiceCollection Loader<T>(IServiceCollection services)
            where T : Module
        {
            if (!_loaded.Contains(GetType()))
            {
                Load(services);
                _loaded.Add(GetType());
            }
            return services;
        }
    }
}
