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
	class ProxyDependencyResolver : IDependencyResolver
	{
		private static volatile object _lock = new object();
		private static CompositionContainer _container;

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

		public object GetService(Type serviceType)
		{
			return Container.GetExportedValueOrDefault<object>(AttributedModelServices.GetContractName(serviceType));
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		private static readonly ProxyDependencyResolver _instance = new ProxyDependencyResolver();
		public static IDependencyResolver Instance
		{
			get { return _instance; }
		}
	}
}
