using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyApi.ElementDefinitions;

namespace ProxyApi
{
	/// <summary>
	/// An interface for factory pattern implementations that creates instances of
	/// <see cref="IControllerDefinition"/> from controller types.
	/// </summary>
	public interface IControllerDefinitionFactory
	{
		/// <summary>
		/// Creates a new <see cref="IControllerDefinition"/> based on <paramref name="controllerType"/>.
		/// </summary>
		/// <param name="controllerType">Type of the controller from which to create the definition.</param>
		/// <returns>A new instance of <see cref="IControllerDefinition"/></returns>
		IControllerDefinition Create(Type controllerType);
	}
}
