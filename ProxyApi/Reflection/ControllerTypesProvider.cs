using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace ProxyApi.Reflection
{
	/// <summary>
	/// A class that provides a list of types that are considered
	/// to be controllers.
	/// </summary>
	[Export(typeof(IControllerTypesProvider))]
	public class ControllerTypesProvider : IControllerTypesProvider
	{
		private IAssemblyProvider _assemblyProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerTypesProvider" /> class.
		/// </summary>
		/// <param name="assemblyProvider">The assembly provider.</param>
		[ImportingConstructor]
		public ControllerTypesProvider(IAssemblyProvider assemblyProvider)
		{
			if (assemblyProvider == null) throw new ArgumentNullException("assemblyProvider");
			
			_assemblyProvider = assemblyProvider;
		}

		/// <summary>
		/// Gets all available types that are considered to be controllers.
		/// </summary>
		/// <returns>
		/// The types that are controllers.
		/// </returns>
		public IEnumerable<Type> GetControllerTypes()
		{
			return _assemblyProvider.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(IsControllerType);
		}

		private static bool IsControllerType(Type type)
		{
			if (type == typeof(ApiController) ||
				type == typeof(Controller) ||
				type == typeof(AsyncController)) return false;

			return typeof(ApiController).IsAssignableFrom(type) ||
				typeof(Controller).IsAssignableFrom(type);
		}
	}

}
