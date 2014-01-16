using System;
using System.Collections.Generic;

namespace ProxyApi.ElementDefinitions
{
	/// <summary>
	/// An interface for objects representing information about a discovered controller.
	/// </summary>
	public interface IControllerDefinition
	{
		/// <summary>
		/// Gets the action methods that appear on the controller.
		/// </summary>
		/// <value>
		/// The action methods.
		/// </value>
		IList<IActionMethodDefinition> ActionMethods { get; }

        IList<IModelDefinition> Models { get; set; }

		/// <summary>
		/// Gets the name of the controller.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the of the controller to be used in URLs
		/// </summary>
		/// <value>
		/// The URL controller name.
		/// </value>
		string UrlName { get; }

		/// <summary>
		/// Gets the type of the controller
		/// </summary>
		ControllerType Type { get; }

        bool ContainsModel(string name);

        string Host { get; }
	}
}
