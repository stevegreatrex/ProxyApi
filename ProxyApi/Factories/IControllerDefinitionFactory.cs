using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyApi.ElementDefinitions;

namespace ProxyApi
{
	public interface IControllerDefinitionFactory
	{
		ControllerDefinition Create(Type controllerType);

		IEnumerable<ControllerDefinition> CreateAll();
	}
}
