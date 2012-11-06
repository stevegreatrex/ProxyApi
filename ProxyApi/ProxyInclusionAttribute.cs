using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyApi
{
	/// <summary>
	/// An attribute for specifying a non-default inclusion rule.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class ProxyInclusionAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyInclusionAttribute" /> class.
		/// </summary>
		public ProxyInclusionAttribute(InclusionRule inclusionRule)
		{
			this.InclusionRule = inclusionRule;
		}

		/// <summary>
		/// Gets the inclusion rule.
		/// </summary>
		/// <value>
		/// The inclusion rule.
		/// </value>
		public InclusionRule InclusionRule { get; private set; }
	}

	/// <summary>
	/// An attribute for explicitly specifying that an element should be included
	/// in the proxy.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class ProxyIncludeAttribute : ProxyInclusionAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyIncludeAttribute" /> class.
		/// </summary>
		public ProxyIncludeAttribute()
			: base(InclusionRule.IncludeAll)
		{}
	}

	/// <summary>
	/// An attribute for explicitly specifying that an element should be excluded
	/// from the proxy.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class ProxyExcludeAttribute : ProxyInclusionAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyExcludeAttribute" /> class.
		/// </summary>
		public ProxyExcludeAttribute()
			: base(InclusionRule.ExcludeAll)
		{}
	}
}
