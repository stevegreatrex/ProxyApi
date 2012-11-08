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
	/// <summary>
	/// The class that generates the proxy script.
	/// </summary>
	[Export(typeof(IProxyGenerator))]
	public class ProxyGenerator : IProxyGenerator
	{
		private IControllerDefinitionFactory _factory;
		private IControllerElementsProvider _typesProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyGenerator" /> class.
		/// </summary>
		/// <param name="typesProvider">The types provider.</param>
		/// <param name="factory">The factory.</param>
		[ImportingConstructor]
		public ProxyGenerator(
			IControllerElementsProvider typesProvider,
			IControllerDefinitionFactory factory)
		{
			if (typesProvider == null) throw new ArgumentNullException("typesProvider");
			if (factory == null) throw new ArgumentNullException("factory");

			_typesProvider	= typesProvider;
			_factory		= factory;
		}

		/// <summary>
		/// Generates the proxy script.
		/// </summary>
		/// <returns>
		/// The script content.
		/// </returns>
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
