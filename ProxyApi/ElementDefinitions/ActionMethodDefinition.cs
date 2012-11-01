using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProxyApi.ElementDefinitions
{
	/// <summary>
	/// A class representing the definition of an action method element.
	/// </summary>
	public class ActionMethodDefinition : IActionMethodDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionMethodDefinition" /> class.
		/// </summary>
		public ActionMethodDefinition()
		{
			this.UrlParameters	= new List<IParameterDefinition>();
			this.Type			= HttpVerbs.Get;
		}

		/// <summary>
		/// Gets the type of HTTP request that should be made.
		/// </summary>
		public HttpVerbs Type { get; set; }

		/// <summary>
		/// Gets the name of the method.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets the MVC/WebApi URL for this definition.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets a list containing the URL parameters.  Parameter values are in the format [index, name]
		/// </summary>
		/// <value>
		/// The URL parameters.
		/// </value>
		public IList<IParameterDefinition> UrlParameters { get; private set; }

		/// <summary>
		/// Gets a list containing the body parameters.  Parameter value is in the format [index, name]
		/// </summary>
		/// <value>
		/// The body parameters.
		/// </value>
		public IParameterDefinition BodyParameter { get; set; }
	}
}
