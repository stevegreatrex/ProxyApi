using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyApi.Templates;

namespace ProxyApi
{
	[Export(typeof(IProxyGenerator))]
	public class ProxyGenerator : IProxyGenerator
	{
		private IControllerDefinitionFactory _factory;
		[ImportingConstructor]
		public ProxyGenerator(IControllerDefinitionFactory factory)
		{
			_factory = factory;
		}

		public string GenerateProxyScript()
		{
			var controllers = _factory.CreateAll();

			var template = new ProxyTemplate();
			
			foreach (var item in controllers)
				template.Definitions.Add(item);

			return template.TransformText();
		}
	}
}
