using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using ProxyApi.Reflection;

namespace ProxyApi
{
	/// <summary>
	/// Default implementation of the <see cref="IProxyGeneratorConfiguration"/> interface.
	/// </summary>
	public sealed class ProxyGeneratorConfiguration : IProxyGeneratorConfiguration
	{
		private static readonly ProxyGeneratorConfiguration _default = new ProxyGeneratorConfiguration();

		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyGeneratorConfiguration" /> class.
		/// </summary>
		public ProxyGeneratorConfiguration()
		{
			this.PathUtility		= new PathUtility(new ContextProvider());
			this.AssemblyProvider	= new AppDomainAssemblyProvider();
		}

		/// <summary>
		/// Gets or sets the default inclusion rule for proxy generation.
		/// </summary>
		/// <value>
		/// The default inclusion rule.
		/// </value>
		public InclusionRule InclusionRule { get; set; }

		/// <summary>
		/// Gets or sets the path utility.
		/// </summary>
		/// <value>
		/// The path utility.
		/// </value>
		[Import]
		public IPathUtility PathUtility { get; set; }

		/// <summary>
		/// Gets or sets the assembly provider.
		/// </summary>
		/// <value>
		/// The assembly provider.
		/// </value>
		[Import]
		public IAssemblyProvider AssemblyProvider { get; set; }
		
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
