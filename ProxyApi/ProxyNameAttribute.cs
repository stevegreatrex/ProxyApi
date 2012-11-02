using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProxyApi
{
	/// <summary>
	/// An attribute class used to explicitly specify the name of a method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class ProxyNameAttribute : Attribute
	{
		private static Regex _validPattern = new Regex("^[a-zA-Z$_]+([a-zA-Z$_0-9])*$");

		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyNameAttribute" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public ProxyNameAttribute(string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			if (!_validPattern.IsMatch(name))
				throw new ArgumentException("Specified name is invalid");

			this.Name = name;
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; private set; }
	}
}
