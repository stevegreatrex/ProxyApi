using ProxyApi.MetadataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProxyApi.MetadataGenerator.Templates
{
	/// <summary>
	/// A partial class implementation used to pass parameters to the proxy template.
	/// </summary>
	public partial class CSharpProxyTemplate
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyTemplate" /> class.
		/// </summary>
        public CSharpProxyTemplate(Configuration config)
		{
            
            this.Configuration = config;
		}

		/// <summary>
		/// Gets the controller definitions that appear in this template.
		/// </summary>
		/// <value>
		/// The template definitions.
		/// </value>

        public bool renderNamespaces { get; set; }
        public Configuration Configuration { get; set; }

    }
}
