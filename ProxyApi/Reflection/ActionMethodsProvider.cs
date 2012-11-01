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

			var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
				.GroupBy(m => m.Name);

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
