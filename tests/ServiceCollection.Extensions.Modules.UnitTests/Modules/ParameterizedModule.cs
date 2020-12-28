namespace ServiceCollection.Extensions.Modules.UnitTests.Modules
{
	using Microsoft.Extensions.DependencyInjection;
	using ServiceCollection.Extensions.Modules.UnitTests.Interfaces;
	using ServiceCollection.Extensions.Modules.UnitTests.Implementations;

	public class ParameterizedModule : Module
	{
		private readonly IService _serviceImpl;
	    
		public ParameterizedModule(IService serviceImpl)
		{
			_serviceImpl = serviceImpl;
		}

		protected override void Load(IServiceCollection services)
		{
			base.Load(services);
			services.AddSingleton<IService2>(new Service2Impl1(_serviceImpl));
		}
	}
}
