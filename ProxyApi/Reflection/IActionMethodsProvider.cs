using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
	/// <summary>
	/// An interface for objects that return the methods that should be considered action
	/// methods on a given controller type.
	/// </summary>
	public interface IActionMethodsProvider
	{
		/// <summary>
		/// Gets the action methods that are found on <paramref name="controllerType"/>.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns>The action methods located on the controller type.</returns>
		IEnumerable<MethodInfo> GetMethods(Type controllerType);
	}
}
