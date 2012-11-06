using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Reflection
{
	/// <summary>
	/// A class that returns the methods that should be considered action
	/// methods on a given controller type.
	/// </summary>
	[Export(typeof(IActionMethodsProvider))]
	public class ActionMethodsProvider : IActionMethodsProvider
	{
		private IProxyGeneratorConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionMethodsProvider" /> class.
		/// </summary>
		[ImportingConstructor]
		public ActionMethodsProvider(IProxyGeneratorConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");

			_configuration = configuration;
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

		private bool IsIncluded(MethodInfo method)
		{
			var rule = _configuration.InclusionRule;

			var attribute = method.GetCustomAttribute<ProxyInclusionAttribute>();
			if (attribute != null)
				rule = attribute.InclusionRule;
			
			return rule == InclusionRule.IncludeAll;
		}

		private IEnumerable<MethodInfo> GetPotentialMethods(Type controllerType)
		{
			var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
				.Where(IsIncluded)
				.GroupBy(m => m.GetProxyName());

			foreach (var methodName in methods)
			{
				if (methodName.Count() > 1)
					yield return methodName.OrderByDescending(m=>m.GetParameters().Length).First();
				else
					yield return methodName.First();
			}
		}
	}
}
