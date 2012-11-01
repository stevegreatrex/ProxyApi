using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Reflection
{
	/// <summary>
	/// An interface for objects that provide a list of types that are considered
	/// to be controllers.
	/// </summary>
	public interface IControllerTypesProvider
	{
		/// <summary>
		/// Gets all available types that are considered to be controllers.
		/// </summary>
		/// <returns>The types that are controllers.</returns>
		IEnumerable<Type> GetControllerTypes();
	}
}
