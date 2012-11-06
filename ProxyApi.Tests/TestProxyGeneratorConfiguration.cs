using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="ProxyGeneratorConfiguration"/> class.
	/// </summary>
	[TestClass]
	public class TestProxyGeneratorConfiguration
	{
		/// <summary>
		/// Ensures that Default is not null
		/// </summary>
		[TestMethod]
		public void Default_Is_Not_Null()
		{
			Assert.IsNotNull(ProxyGeneratorConfiguration.Default);
		}

		/// <summary>
		/// Ensures that InclusionRule can be get and set as expected
		/// </summary>
		[TestMethod]
		public void InclusionRule_Get_Set_Behaviour()
		{
			Assert.AreEqual(InclusionRule.IncludeAll, ProxyGeneratorConfiguration.Default.InclusionRule);
			ProxyGeneratorConfiguration.Default.InclusionRule = InclusionRule.ExcludeAll;
			Assert.AreEqual(InclusionRule.ExcludeAll, ProxyGeneratorConfiguration.Default.InclusionRule);
		}
	}
}
