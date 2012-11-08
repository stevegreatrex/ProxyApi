using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyApi.Reflection;

namespace ProxyApi
{
	/// <summary>
	/// An interface for objects containing configuration data for the proxy generator.
	/// </summary>
	public interface IProxyGeneratorConfiguration
	{
		/// <summary>
		/// Gets or sets the default inclusion rule for proxy generation.
		/// </summary>
		/// <value>
		/// The default inclusion rule.
		/// </value>
		InclusionRule InclusionRule { get; set; }

		/// <summary>
		/// Gets or sets the utility used to manipulate URLs & paths.
		/// </summary>
		/// <value>
		/// The path utility.
		/// </value>
		IPathUtility PathUtility { get; set; }

		/// <summary>
		/// Gets or sets the assembly provider.
		/// </summary>
		/// <value>
		/// The assembly provider.
		/// </value>
		IAssemblyProvider AssemblyProvider { get; set; }
	}
}
