using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyApi.Reflection;
using ProxyApi.Templates;

namespace ProxyApi
{
	[Export(typeof(IProxyGenerator))]
	public class ProxyGenerator : IProxyGenerator
	{
		private IControllerDefinitionFactory _factory;
		private IControllerTypesProvider _typesProvider;

		[ImportingConstructor]
		public ProxyGenerator(
			IControllerTypesProvider typesProvider,
			IControllerDefinitionFactory factory)
		{
			_typesProvider	= typesProvider;
			_factory		= factory;
		}

		public string GenerateProxyScript()
		{
			var controllers = _typesProvider.GetControllerTypes()
				.Select(_factory.Create)
				.ToList();

			var template = new ProxyTemplate();
			
			foreach (var item in controllers)
				template.Definitions.Add(item);

			return template.TransformText();
		}
	}
}
