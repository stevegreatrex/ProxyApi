using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ProxyApi.ElementDefinitions;
using ProxyApi.Reflection;

namespace ProxyApi
{
	[Export(typeof(IControllerDefinitionFactory))]
	public class ControllerDefinitionFactory : IControllerDefinitionFactory
	{
		private IActionMethodDefinitionFactory _actionFactory;
		private IActionMethodsProvider _actionProvider;
		private IControllerTypesProvider _typeProvider;
		[ImportingConstructor]
		public ControllerDefinitionFactory(
			IControllerTypesProvider typeProvider,
			IActionMethodsProvider actionProvider,
			IActionMethodDefinitionFactory actionFactory)
		{
			_typeProvider = typeProvider;
			_actionProvider = actionProvider;
			_actionFactory = actionFactory;
		}

		public ControllerDefinition Create(Type controllerType)
		{
			var definition = new ControllerDefinition();
			definition.Name = controllerType.Name.ToLower().Replace("controller", "");
			definition.Type = typeof(ApiController).IsAssignableFrom(controllerType) ? ControllerType.WebApi : ControllerType.Mvc;

			foreach (var method in _actionProvider.GetMethods(controllerType))
			{
				var methodDefinition = _actionFactory.Create(definition, method);
				definition.ActionMethods.Add(methodDefinition);
			}

			return definition;
		}


		public IEnumerable<ControllerDefinition> CreateAll()
		{
			return _typeProvider.GetControllerTypes().Select(Create);
		}
	}
}
