using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Reflection
{
	/// <summary>
	/// An interface for objects that provide a list of elements that are considered
	/// to be controllers or actions.
	/// </summary>
	public interface IControllerElementsProvider
	{
		/// <summary>
		/// Gets all available types that are considered to be controllers.
		/// </summary>
		/// <returns>The types that are controllers.</returns>
		IEnumerable<Type> GetControllerTypes();

		/// <summary>
		/// Gets the action methods that are found on <paramref name="controllerType"/>.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns>The action methods located on the controller type.</returns>
		IEnumerable<MethodInfo> GetMethods(Type controllerType);
	}
}
