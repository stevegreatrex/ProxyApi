using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyApi.ElementDefinitions
{
	/// <summary>
	/// An interface for objects representing an argument to a method.
	/// </summary>
	public interface IParameterDefinition
	{
		/// <summary>
		/// Gets the name of the argument.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get; }

		/// <summary>
		/// Gets the position at which the argument appears in the parameters list.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		int Index { get; }

		/// <summary>
		/// Gets the type of the param.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		Type Type { get; }

	}
}
