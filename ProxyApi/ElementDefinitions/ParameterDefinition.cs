using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyApi.ElementDefinitions
{
	/// <summary>
	/// A class representing the argument to a method
	/// </summary>
	public class ParameterDefinition : IParameterDefinition
	{
		/// <summary>
		/// Gets or sets the name of the argument.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the position at which the argument appears in the parameters list.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		public int Index { get; set; }
	}
}
