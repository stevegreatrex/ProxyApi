using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
	[Export(typeof(IActionMethodsProvider))]
	public class ActionMethodsProvider : IActionMethodsProvider
	{
		public IEnumerable<MethodInfo> GetMethods(Type controllerType)
		{
			var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
				.GroupBy(m => m.Name);

			foreach (var methodName in methods)
			{
				if (methodName.Count() > 1)
					yield return methodName.OrderByDescending(m=>m.GetParameters().Length).First();
				else
					yield return methodName.First();
			}
		}
	}
}
