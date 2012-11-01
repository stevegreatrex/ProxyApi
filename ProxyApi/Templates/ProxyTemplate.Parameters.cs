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
	public partial class ProxyTemplate
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyTemplate" /> class.
		/// </summary>
		public ProxyTemplate()
		{
			this.Definitions = new List<IControllerDefinition>();
		}

		/// <summary>
		/// Gets the controller definitions that appear in this template.
		/// </summary>
		/// <value>
		/// The template definitions.
		/// </value>
		public IList<IControllerDefinition> Definitions { get; private set; }
	}
}
