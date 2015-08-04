using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyApi
{
	public static class JSDocTypeExtension
	{
		public static string JsDocTypeString(this Type type)
		{
			if (type == typeof(String))
				return "String";

			if (type.IsArray)
				return "Array";

			if (type.IsPrimitive)
			{
				if (type == typeof(bool))
					return "Boolean";

				if (type == typeof(int)
				|| type == typeof(double)
				|| type == typeof(float)
				|| type == typeof(decimal)
				|| type == typeof(uint)
				|| type == typeof(short)
				|| type == typeof(ushort)
				|| type == typeof(long)
				|| type == typeof(ulong)
				|| type == typeof(bool))
					return "Number";

			}

			return "Object";
		}
	}
}
