using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ServiceCollection.Extensions.Modules
{
    using Microsoft.Extensions.DependencyInjection;

    public abstract class Module
    {
        private static readonly ConcurrentDictionary<Type, HashSet<int>> Loaded =
            new ConcurrentDictionary<Type, HashSet<int>>();

        protected internal Module()
        {
        }

        protected virtual void Load(IServiceCollection services)
        {
        }

        internal IServiceCollection Loader(IServiceCollection services)
        {
            if (!Loaded.TryGetValue(GetType(), out var s))
            {
                Load(services);
                Loaded.TryAdd(GetType(), new HashSet<int>() { services.GetHashCode() });
            }
            else if (!s.Contains(services.GetHashCode()))
            {
                Load(services);
                s.Add(services.GetHashCode());
            }

            return services;
        }
    }
}