using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
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
	[Export(typeof(IControllerElementsProvider))]
	public class ControllerElementsProvider : IControllerElementsProvider
	{
		private IAssemblyProvider _assemblyProvider;
		private IProxyGeneratorConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerElementsProvider" /> class.
		/// </summary>
		/// <param name="assemblyProvider">The assembly provider.</param>
		[ImportingConstructor]
		public ControllerElementsProvider(IAssemblyProvider assemblyProvider, IProxyGeneratorConfiguration configuration)
		{
			if (assemblyProvider == null) throw new ArgumentNullException("assemblyProvider");
			if (configuration == null) throw new ArgumentNullException("configuration");
			
			_assemblyProvider	= assemblyProvider;
			_configuration		= configuration;
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
				.SelectMany(GetAssemblyTypes)
				.Where(t => !t.IsAbstract)
				.Where(IsControllerType)
				.Where(IsIncluded);
		}

		/// <summary>
		/// Gets the action methods that are found on <paramref name="controllerType" />.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns>
		/// The action methods located on the controller type.
		/// </returns>
		public IEnumerable<MethodInfo> GetMethods(Type controllerType)
		{
			if (controllerType == null) throw new ArgumentNullException("controllerType");

			return GetPotentialMethods(controllerType);
		}

		private IEnumerable<Type> GetAssemblyTypes(Assembly assembly)
		{
			try
			{
				return assembly.GetTypes();
			}
			catch
			{
				return Enumerable.Empty<Type>();
			}
		}

		private bool IsIncluded(MethodInfo method, Type controllerType)
		{
			var rule = _configuration.InclusionRule;

			var controllerAttribute = controllerType.GetCustomAttribute<ProxyInclusionAttribute>();
			if (controllerAttribute != null)
				rule = controllerAttribute.InclusionRule;

			var methodAttribute = method.GetCustomAttribute<ProxyInclusionAttribute>();
			if (methodAttribute != null)
				rule = methodAttribute.InclusionRule;
			
			return rule == InclusionRule.IncludeAll;
		}

		private IEnumerable<MethodInfo> GetPotentialMethods(Type controllerType)
		{
			var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
				.Where(m => IsIncluded(m, controllerType))
				.GroupBy(m => m.GetProxyName());

			foreach (var methodName in methods)
			{
				if (methodName.Count() > 1)
					yield return methodName.OrderByDescending(m=>m.GetParameters().Length).First();
				else
					yield return methodName.First();
			}
		}

		private static bool IsControllerType(Type type)
		{
			if (type == typeof(ApiController) ||
				type == typeof(Controller) ||
				type == typeof(AsyncController)) return false;

			return typeof(ApiController).IsAssignableFrom(type) ||
				typeof(Controller).IsAssignableFrom(type);
		}

		private bool IsIncluded(Type type)
		{
			var rule = _configuration.InclusionRule;

			var attribute = type.GetCustomAttribute<ProxyInclusionAttribute>();
			if (attribute != null)
				rule = attribute.InclusionRule;
			
			return rule == InclusionRule.IncludeAll;
		}
	}

}
