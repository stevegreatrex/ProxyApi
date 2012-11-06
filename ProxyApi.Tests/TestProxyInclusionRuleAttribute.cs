using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="ProxyInclusionRuleAttribute"/> class.
	/// </summary>
	[TestClass]
	public class TestProxyInclusionRuleAttribute
	{
		/// <summary>
		/// Ensures that Constructor sets InclusionRule
		/// </summary>
		[TestMethod]
		public void Constructor_Sets_InclusionRule()
		{
			var testSubject = new ProxyInclusionAttribute(InclusionRule.ExcludeAll);
			Assert.AreEqual(InclusionRule.ExcludeAll, testSubject.InclusionRule);
		}

		/// <summary>
		/// Ensures that ProxyIncludeAttribute sets IncludeAll
		/// </summary>
		[TestMethod]
		public void ProxyIncludeAttribute_Sets_IncludeAll()
		{
			var testSubject = new ProxyIncludeAttribute();
			Assert.AreEqual(InclusionRule.IncludeAll, testSubject.InclusionRule);
		}

		/// <summary>
		/// Ensures that ProxyExcludeAttribute sets ExcludeAll
		/// </summary>
		[TestMethod]
		public void ProxyExcludeAttribute_Sets_ExcludeAll()
		{
			var testSubject = new ProxyExcludeAttribute();
			Assert.AreEqual(InclusionRule.ExcludeAll, testSubject.InclusionRule);
		}
	}
}
