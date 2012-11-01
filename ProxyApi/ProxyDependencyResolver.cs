using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProxyApi
{
	/// <summary>
	/// A <see cref="IDependencyResolver"/> implementation specific to
	/// the proxy API generator.
	/// </summary>
	public class ProxyDependencyResolver : IDependencyResolver
	{
		private static volatile object _lock = new object();
		private static CompositionContainer _container;
		private static readonly ProxyDependencyResolver _instance = new ProxyDependencyResolver();

		private static CompositionContainer Container
		{
			get
			{
				if (_container == null)
					lock(_lock)
						if (_container == null)
						{
							_container = new CompositionContainer(new AssemblyCatalog(typeof(ProxyDependencyResolver).Assembly));
						}

				return _container;
			}
		}

		/// <summary>
		/// Resolves singly registered services that support arbitrary object creation.
		/// </summary>
		/// <param name="serviceType">The type of the requested service or object.</param>
		/// <returns>
		/// The requested service or object.
		/// </returns>
		public object GetService(Type serviceType)
		{
			return Container.GetExportedValueOrDefault<object>(AttributedModelServices.GetContractName(serviceType));
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
			return Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

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
	}
}
