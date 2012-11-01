using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ProxyApi.Reflection
{
	public static class AttributeExtensions
	{
		public static TAttribute GetCustomAttribute<TAttribute>(this MethodInfo method, bool inherit = true)
			where TAttribute : Attribute
		{
			return method.GetCustomAttributes(inherit).OfType<TAttribute>().FirstOrDefault();
		}

		public static TAttribute GetCustomAttribute<TAttribute>(this ParameterInfo parameter, bool inherit = true)
			where TAttribute : Attribute
		{
			return parameter.GetCustomAttributes(inherit).OfType<TAttribute>().FirstOrDefault();
		}
		
		public static bool HasAttribute<TAttribute>(this MethodInfo method, bool inherit = true)
			where TAttribute : Attribute
		{
			return method.GetCustomAttribute<TAttribute>(inherit) != null;
		}

		public static bool HasAttribute<TAttribute>(this ParameterInfo parameter, bool inherit = true)
			where TAttribute : Attribute
		{
			return parameter.GetCustomAttribute<TAttribute>(inherit) != null;
		}
	}
}
