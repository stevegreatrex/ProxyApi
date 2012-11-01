using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ProxyApi.ElementDefinitions;
using ProxyApi.Reflection;

namespace ProxyApi.Factories
{
	/// <summary>
	/// A factory pattern implementation that creates instances of <see cref="IControllerDefinition"/>
	/// from controller types.
	/// </summary>
	[Export(typeof(IControllerDefinitionFactory))]
	public class ControllerDefinitionFactory : IControllerDefinitionFactory
	{
		private IActionMethodDefinitionFactory _actionFactory;
		private IActionMethodsProvider _actionProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerDefinitionFactory" /> class.
		/// </summary>
		/// <param name="actionProvider">The action provider.</param>
		/// <param name="actionFactory">The action factory.</param>
		[ImportingConstructor]
		public ControllerDefinitionFactory(
			IActionMethodsProvider actionProvider,
			IActionMethodDefinitionFactory actionFactory)
		{
			if (actionProvider == null) throw new ArgumentNullException("actionProvider");
			if (actionFactory == null) throw new ArgumentNullException("actionFactory");

			_actionProvider	= actionProvider;
			_actionFactory	= actionFactory;
		}

		/// <summary>
		/// Creates a new <see cref="IControllerDefinition"/> based on <paramref name="controllerType"/>.
		/// </summary>
		/// <param name="controllerType">Type of the controller from which to create the definition.</param>
		/// <returns>A new instance of <see cref="IControllerDefinition"/></returns>
		public IControllerDefinition Create(Type controllerType)
		{
			if (controllerType == null) throw new ArgumentNullException("controllerType");

			var definition	= new ControllerDefinition();
			definition.Name	= controllerType.Name.ToLower().Replace("controller", string.Empty);
			definition.Type	= typeof(ApiController).IsAssignableFrom(controllerType) ? ControllerType.WebApi : ControllerType.Mvc;

			foreach (var method in _actionProvider.GetMethods(controllerType))
				definition.ActionMethods.Add(_actionFactory.Create(definition, method));

			return definition;
		}
	}
}
