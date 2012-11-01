using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
	/// <summary>
	/// An interface for classes that generate the proxy scripts.
	/// </summary>
	public interface IProxyGenerator
	{
		/// <summary>
		/// Generates the proxy script.
		/// </summary>
		/// <returns>The script content.</returns>
		string GenerateProxyScript();
	}
}
