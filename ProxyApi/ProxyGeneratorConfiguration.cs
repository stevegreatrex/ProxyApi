using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace ProxyApi
{
	/// <summary>
	/// Default implementation of the <see cref="IProxyGeneratorConfiguration"/> interface.
	/// </summary>
	public sealed class ProxyGeneratorConfiguration : IProxyGeneratorConfiguration
	{
		private static readonly ProxyGeneratorConfiguration _default = new ProxyGeneratorConfiguration();
		
		/// <summary>
		/// Gets or sets the default inclusion rule for proxy generation.
		/// </summary>
		/// <value>
		/// The default inclusion rule.
		/// </value>
		public InclusionRule InclusionRule { get; set; }
		
		/// <summary>
		/// Gets the default configuration.
		/// </summary>
		/// <value>
		/// The default configuration.
		/// </value>
		[Export(typeof(IProxyGeneratorConfiguration))]
		public static IProxyGeneratorConfiguration Default
		{
			get { return _default; }
		}
	}
}
