using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProxyApi.Factories;
using ProxyApi.Reflection;

namespace ProxyApi
{
	/// <summary>
	/// A <see cref="IDependencyResolver"/> implementation specific to
	/// the proxy API generator.
	/// </summary>
	public class ProxyDependencyResolver : IDependencyResolver
	{
		private readonly IProxyGeneratorConfiguration _configuration;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyDependencyResolver" /> class.
		/// </summary>
		public ProxyDependencyResolver()
			: this(ProxyGeneratorConfiguration.Default)
		{}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyDependencyResolver" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public ProxyDependencyResolver(IProxyGeneratorConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");

			_configuration = configuration;
		}

		#endregion

		#region IDependencyResolver Members

		/// <summary>
		/// Resolves singly registered services that support arbitrary object creation.
		/// </summary>
		/// <param name="serviceType">The type of the requested service or object.</param>
		/// <returns>
		/// The requested service or object.
		/// </returns>
		public object GetService(Type serviceType)
		{
			if (typeof(IPathUtility) == serviceType)
				return _configuration.PathUtility ?? new PathUtility(new ContextProvider());

			if (typeof(IAssemblyProvider) == serviceType)
				return _configuration.AssemblyProvider ?? new AppDomainAssemblyProvider();

			if (typeof(IProxyGenerator) == serviceType)
				return new ProxyGenerator(
						GetService<IControllerElementsProvider>(),
						GetService<IControllerDefinitionFactory>());

			if (typeof(IControllerElementsProvider) == serviceType)
				return new ControllerElementsProvider(
					GetService<IAssemblyProvider>(),
					_configuration);

			if (typeof(IControllerDefinitionFactory) == serviceType)
				return new ControllerDefinitionFactory(
					GetService<IControllerElementsProvider>(),
					GetService<IActionMethodDefinitionFactory>());

			if (typeof(IActionMethodDefinitionFactory) == serviceType)
				return new ActionMethodDefinitionFactory(
					GetService<IPathUtility>());

			if (typeof(RouteHandler) == serviceType)
				return new RouteHandler(GetService<IProxyGenerator>());

			return null;
		}

		private TService GetService<TService>()
		{
			return (TService)this.GetService(typeof(TService));
		}

		/// <summary>
		/// Resolves multiply registered services.
		/// </summary>
		/// <param name="serviceType">The type of the requested services.</param>
		/// <returns>
		/// The requested services.
		/// </returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			yield return GetService(serviceType);;
		}

		#endregion

		#region Singleton Implementation

		private static readonly ProxyDependencyResolver _instance = new ProxyDependencyResolver();

		/// <summary>
		/// Gets the singleton instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public static IDependencyResolver Instance
		{
			get { return _instance; }
		}

		#endregion
	}
}
