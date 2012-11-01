using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ProxyApi.Reflection
{
	/// <summary>
	/// Extension methods to help with checking on custom attributes.
	/// </summary>
	public static class AttributeExtensions
	{
		/// <summary>
		/// Gets the custom attribute instance of type <typeparamref name="TAttribute"/> on
		/// <paramref name="method"/>, if one exists.
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute for which to look.</typeparam>
		/// <param name="method">The method on which to check.</param>
		/// <param name="inherit">if set to <c>true</c>, inherited attributes will be considered.</param>
		/// <returns>The matching attribute, or <c>null</c></returns>
		public static TAttribute GetCustomAttribute<TAttribute>(this MethodInfo method, bool inherit = true)
			where TAttribute : Attribute
		{
			if (method == null) throw new ArgumentNullException("method");

			return method.GetCustomAttributes(inherit).OfType<TAttribute>().FirstOrDefault();
		}

		/// <summary>
		/// Gets the custom attribute instance of type <typeparamref name="TAttribute"/> on
		/// <paramref name="parameter"/>, if one exists.
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute for which to look.</typeparam>
		/// <param name="parameter">The method on which to check.</param>
		/// <param name="inherit">if set to <c>true</c>, inherited attributes will be considered.</param>
		/// <returns>The matching attribute, or <c>null</c></returns>
		public static TAttribute GetCustomAttribute<TAttribute>(this ParameterInfo parameter, bool inherit = true)
			where TAttribute : Attribute
		{
			if (parameter == null) throw new ArgumentNullException("parameter");

			return parameter.GetCustomAttributes(inherit).OfType<TAttribute>().FirstOrDefault();
		}
		
		/// <summary>
		/// Returns true if an instance of type <typeparamref name="TAttribute"/> exists on
		/// <paramref name="method"/>.
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute for which to look.</typeparam>
		/// <param name="parameter">The method on which to check.</param>
		/// <param name="inherit">if set to <c>true</c>, inherited attributes will be considered.</param>
		/// <returns><c>true</c> if a matching attribute exists</returns>
		public static bool HasAttribute<TAttribute>(this MethodInfo method, bool inherit = true)
			where TAttribute : Attribute
		{
			if (method == null) throw new ArgumentNullException("method");

			return method.GetCustomAttribute<TAttribute>(inherit) != null;
		}

		/// <summary>
		/// Returns true if an instance of type <typeparamref name="TAttribute"/> exists on
		/// <paramref name="method"/>.
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute for which to look.</typeparam>
		/// <param name="parameter">The method on which to check.</param>
		/// <param name="inherit">if set to <c>true</c>, inherited attributes will be considered.</param>
		/// <returns><c>true</c> if a matching attribute exists</returns>
		public static bool HasAttribute<TAttribute>(this ParameterInfo parameter, bool inherit = true)
			where TAttribute : Attribute
		{
			if (parameter == null) throw new ArgumentNullException("parameter");

			return parameter.GetCustomAttribute<TAttribute>(inherit) != null;
		}
	}
}
