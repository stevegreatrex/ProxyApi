using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace ProxyApi
{
	[Export(typeof(IControllerTypesProvider))]
	public class ControllerTypesProvider : IControllerTypesProvider
	{
		private IAssemblyProvider _assemblyProvider;

		[ImportingConstructor]
		public ControllerTypesProvider(IAssemblyProvider assemblyProvider)
		{
			_assemblyProvider = assemblyProvider;
		}

		public IEnumerable<Type> GetControllerTypes()
		{
			return _assemblyProvider.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(IsControllerType);
		}

		private static bool IsControllerType(Type type)
		{
			if (type == typeof(ApiController) || type == typeof(Controller)) return false;

			return typeof(ApiController).IsAssignableFrom(type) ||
				typeof(Controller).IsAssignableFrom(type);
		}
	}

}
