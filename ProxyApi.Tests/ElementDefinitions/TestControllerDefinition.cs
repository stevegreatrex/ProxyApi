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
	/// Tests the functionality of the <see cref="ControllerDefinition"/> class.
	/// </summary>	
	[TestClass]
	public class TestControllerDefinition : FixtureBase<ControllerDefinition>
	{
		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ControllerDefinition"/> for each
		/// unit test.
		/// </summary>
		public override ControllerDefinition CreateTestSubject()
		{
			return new ControllerDefinition();
		}

		#endregion

		#region Tests

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
		/// Ensures that ActionMethods is an empty collection
		/// </summary>
		[TestMethod]
		public void ActionMethods_Is_Empty()
		{
			Assert.IsNotNull(this.TestSubject.ActionMethods);
			Assert.AreEqual(0, this.TestSubject.ActionMethods.Count);
		}

		/// <summary>
		/// Ensures that the UrlName property can be set & retrieved
		/// </summary>
		[TestMethod]
		public void UrlName_Can_Be_Set_And_Retrieved()
		{
			Assert.AreEqual(null, this.TestSubject.UrlName);
			this.TestSubject.UrlName = "value";
			Assert.AreEqual("value", this.TestSubject.UrlName);
		}
		
		#endregion
	}
}
