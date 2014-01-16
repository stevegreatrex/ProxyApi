using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyApi.ElementDefinitions;
using System.Web;

namespace ProxyApi.Templates
{
	/// <summary>
	/// A partial class implementation used to pass parameters to the proxy template.
	/// </summary>
	public partial class MetadataTemplate:IProxyTemplate
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyTemplate" /> class.
		/// </summary>
        public MetadataTemplate()
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

        public string Host
        {
            get
            {
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
            }
        }

    }
}
