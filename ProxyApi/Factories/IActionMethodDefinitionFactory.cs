using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProxyApi.ElementDefinitions;

namespace ProxyApi.Factories
{
	/// <summary>
	/// An interface for factory class implementations used to create instances
	/// of <see cref="IActionMethodDefinition"/>.
	/// </summary>
	public interface IActionMethodDefinitionFactory
	{
		/// <summary>
		/// Creates a new <see cref="IActionMethodDefinition"/> in the context of <paramref name="controllerDefinition"/>
		/// for <paramref name="method"/>.
		/// </summary>
		/// <param name="controllerDefinition">The controller definition in which the method is found.</param>
		/// <param name="method">The method for which to create a definition.</param>
		/// <returns>A new <see cref="IActionMethodDefinition"/> instance.</returns>
		IActionMethodDefinition Create(IControllerDefinition controllerDefinition, MethodInfo method);
	}
}
