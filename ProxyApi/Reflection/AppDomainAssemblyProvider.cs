using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
	/// <summary>
	/// An implementation of <see cref="IAssemblyProvider"/> that returns all assemblies in the current
	/// app domain.
	/// </summary>
	[Export(typeof(IAssemblyProvider))]
	public class AppDomainAssemblyProvider : IAssemblyProvider
	{
		/// <summary>
		/// Gets the available assemblies.
		/// </summary>
		/// <returns>
		/// The available assemblies.
		/// </returns>
		public IEnumerable<Assembly> GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}
	}
}
