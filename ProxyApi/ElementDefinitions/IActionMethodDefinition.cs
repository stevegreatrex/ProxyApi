using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ProxyApi.ElementDefinitions
{
	/// <summary>
	/// An interface for objects representing an action method element.
	/// </summary>
	public interface IActionMethodDefinition
	{
		/// <summary>
		/// Gets the type of HTTP request that should be made.
		/// </summary>
		HttpMethod Type { get; }
		
		/// <summary>
		/// Gets the name of the method.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the MVC/WebApi URL for this definition.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Gets a list containing the URL parameters.  Parameter values are in the format [index, name]
		/// </summary>
		/// <value>
		/// The URL parameters.
		/// </value>
		IList<IParameterDefinition> UrlParameters { get; }
		
		/// <summary>
		/// Gets a list containing the body parameters.  Parameter value is in the format [index, name]
		/// </summary>
		/// <value>
		/// The body parameters.
		/// </value>
		IParameterDefinition BodyParameter { get; }
	}
}
