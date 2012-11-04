using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.ElementDefinitions
{
	/// <summary>
	/// A class representing information about a discovered controller.
	/// </summary>
	public class ControllerDefinition : IControllerDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerDefinition" /> class.
		/// </summary>
		public ControllerDefinition()
		{
			this.ActionMethods = new List<IActionMethodDefinition>();
		}

		/// <summary>
		/// Gets the name of the controller.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets the of the controller to be used in URLs
		/// </summary>
		/// <value>
		/// The URL controller name.
		/// </value>
		public string UrlName { get; set; }
		
		/// <summary>
		/// Gets the action methods that appear on the controller.
		/// </summary>
		/// <value>
		/// The action methods.
		/// </value>
		public IList<IActionMethodDefinition> ActionMethods { get; private set; }
		
		/// <summary>
		/// Gets the type of the controller
		/// </summary>
		public ControllerType Type { get; set; }
	}
}
