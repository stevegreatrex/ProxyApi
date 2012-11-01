using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyApi.ElementDefinitions;

namespace ProxyApi.Tests.ElementDefinitions
{
	/// <summary>
	/// Tests the functionality of the <see cref="ParameterDefinition"/> class.
	/// </summary>
	[TestClass]
	public class TestParameterDefinition : FixtureBase<ParameterDefinition>
	{
		public override ParameterDefinition CreateTestSubject()
		{
			return new ParameterDefinition();
		}

		/// <summary>
		/// Ensures that the Name property can be set & retrieved
		/// </summary>
		[TestMethod]
		public void Name_Can_Be_Set_And_Retrieved()
		{
			Assert.AreEqual(null, this.TestSubject.Name);
			this.TestSubject.Name = "value";
			Assert.AreEqual("value", this.TestSubject.Name);
		}

		/// <summary>
		/// Ensures that the Index property can be set & retrieved
		/// </summary>
		[TestMethod]
		public void Index_Can_Be_Set_And_Retrieved()
		{
			Assert.AreEqual(0, this.TestSubject.Index);
			this.TestSubject.Index = 123;
			Assert.AreEqual(123, this.TestSubject.Index);
		}
	}
}
