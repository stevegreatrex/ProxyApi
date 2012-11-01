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
			this.Definitions = new List<ControllerDefinition>();
		}

		public IList<ControllerDefinition> Definitions { get; private set; }
	}
}
