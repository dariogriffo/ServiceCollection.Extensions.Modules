[![N|Solid](https://avatars2.githubusercontent.com/u/39886363?s=200&v=4)](https://github.com/griffo-io/ServiceCollection.Extensions.Modules)


# ServiceCollection.Extensions.Modules
We all love to have modules to simplify registrations on our DI framework of choice.

Sadly [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/) doesn't have builtin modules.

Here is a naive, simple implementation that has been working for me.

You cannot register twice the same module in the same IServiceCollection, but you can register the same Module across different Collections.

# How to use it

1. Inherit from ServiceCollection.Extensions.Modules.Module class
  
```
namespace MyFanceModulesTest
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules;

    public class MyFancyModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            base.Load(services);
            services.AddSingleton<TSERVICE, TSERVICEIMPL>();            
        }
    }
}
```

2. Register your module

```
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.RegisterModule<MyFancyModule>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // More stuff here...
    }
}
```
- **Boom!** modules, modules, modules!!

# Nested Modules
You can register modules inside modules....

```
namespace MyFancyNestedModulesTest
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules;

    public class InnerModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            base.Load(services); 
            // SOME FANCY REGISTRATIONS HERE
        }
    }

    public class OuterModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            base.Load(services);
            services.RegisterModule<InnerModule>();
            // MORE FANCY REGISTRATIONS HERE
         }
    }
}
```

# Parameterized Modules
You can register instances of modules if you need some parameter....

```
namespace MyParameterizedModulesTest
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules;

    public class ParameterizedModule : Module
    {
        private readonly string _url;

        public ParameterizedModule(string url)
        {
        	_url = _url;
        }
		
        protected override void Load(IServiceCollection services)
        {
            base.Load(services); 
            // SOME FANCY REGISTRATIONS HERE using _url
        }
    }

    // and later on
    services.RegisterModule(new ParameterizedModule("https://google.com"));
}
```
  
 

# Modules registration via Action
An action can be provided to register modules....

```
namespace MyParameterizedModulesTest
{
    using Microsoft.Extensions.DependencyInjection;
    using ServiceCollection.Extensions.Modules;

    public class ParameterizedModule : Module
    {
        private readonly string _url;

        public ParameterizedModule(string url)
        {
        	_url = _url;
        }
		
        protected override void Load(IServiceCollection services)
        {
            base.Load(services); 
            // SOME FANCY REGISTRATIONS HERE using _url
        }
    }

    // and later on
    services
        .RegisterModule(
		(c) =>
	        {
                    var configuration = c.BuildServiceProvider().GetRequiredService<IConfiguration>();
                    return new ParameterizedModule(configuration["ApiUrl"]);
	        });
}
```

Logo Provided by [Vecteezy](https://vecteezy.com)
