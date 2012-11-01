using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
	public interface IActionMethodsProvider
	{
		IEnumerable<MethodInfo> GetMethods(Type controllerType);
	}
}
