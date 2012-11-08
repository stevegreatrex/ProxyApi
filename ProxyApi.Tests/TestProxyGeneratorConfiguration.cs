using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
		/// Ensures that PathUtility defaults to a Null instance
		/// </summary>
		[TestMethod]
		public void PathUtility_Defaults_To_PathUtility()
		{
			var testSubject = new ProxyGeneratorConfiguration();
			Assert.IsNotNull(testSubject.PathUtility);
			Assert.AreEqual(typeof(PathUtility), testSubject.PathUtility.GetType());
			
			var newUtility = new Mock<IPathUtility>().Object;
			testSubject.PathUtility = newUtility;
			Assert.AreEqual(newUtility, testSubject.PathUtility);
		}

		/// <summary>
		/// Ensures that InclusionRule can be get and set as expected
		/// </summary>
		[TestMethod]
		public void InclusionRule_Get_Set_Behaviour()
		{
			var testSubject = new ProxyGeneratorConfiguration();
			Assert.AreEqual(InclusionRule.IncludeAll,testSubject.InclusionRule);
			testSubject.InclusionRule = InclusionRule.ExcludeAll;
			Assert.AreEqual(InclusionRule.ExcludeAll, testSubject.InclusionRule);
		}
	}
}
