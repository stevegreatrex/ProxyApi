using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyApi.ElementDefinitions;

namespace ProxyApi.Templates
{
	/// <summary>
	/// A partial class implementation used to pass parameters to the proxy template.
	/// </summary>
	public partial class JsProxyTemplate:IProxyTemplate
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyTemplate" /> class.
		/// </summary>
		public JsProxyTemplate()
		{
			this.Definitions = new List<IControllerDefinition>();
		}

		/// <summary>
		/// Gets the controller definitions that appear in this template.
		/// </summary>
		/// <value>
		/// The template definitions.
		/// </value>
		public IEnumerable<IControllerDefinition> Definitions { get; set; }

      

    }
}
