using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProxyApi.ElementDefinitions;

namespace ProxyApi
{
	public interface IActionMethodDefinitionFactory
	{
		ActionMethodDefinition Create(ControllerDefinition controllerDefinition, MethodInfo method);
	}
}
