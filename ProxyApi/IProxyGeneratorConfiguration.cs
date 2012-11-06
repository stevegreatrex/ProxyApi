using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
