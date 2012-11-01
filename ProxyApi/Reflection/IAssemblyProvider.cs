using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
	/// <summary>
	/// An interface for objects that provide a list of available assemblies.
	/// </summary>
	public interface IAssemblyProvider
	{
		/// <summary>
		/// Gets the available assemblies.
		/// </summary>
		/// <returns>The available assemblies.</returns>
		IEnumerable<Assembly> GetAssemblies();
	}
}
