using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyApi.ElementDefinitions;

namespace ProxyApi.Tests.ElementDefinitions
{
	/// <summary>
	/// Tests the functionality of the <see cref="ActionMethodDefinition"/> class.
	/// </summary>
	[TestClass]
	public class TestActionMethodDefinition : FixtureBase<ActionMethodDefinition>
	{
		public override ActionMethodDefinition CreateTestSubject()
		{
			return new ActionMethodDefinition();
		}

		/// <summary>
		/// Ensures that the Type property can be set & retrieved
		/// </summary>
		[TestMethod]
		public void TypeCan_Be_Set_And_Retrieved()
		{
			Assert.AreEqual(HttpVerbs.Get, this.TestSubject.Type);
			this.TestSubject.Type = HttpVerbs.Put;
			Assert.AreEqual(HttpVerbs.Put, this.TestSubject.Type);
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
		/// Ensures that the Url property can be set & retrieved
		/// </summary>
		[TestMethod]
		public void Url_Can_Be_Set_And_Retrieved()
		{
			Assert.AreEqual(null, this.TestSubject.Url);
			this.TestSubject.Url = "value";
			Assert.AreEqual("value", this.TestSubject.Url);
		}

		/// <summary>
		/// Ensures that the BodyParameter property can be set & retrieved
		/// </summary>
		[TestMethod]
		public void BodyParameter_Can_Be_Set_And_Retrieved()
		{
			Assert.AreEqual(null, this.TestSubject.BodyParameter);
			var newValue = new ParameterDefinition();
			this.TestSubject.BodyParameter = newValue;
			Assert.AreEqual(newValue, this.TestSubject.BodyParameter);
		}

		/// <summary>
		/// Ensures that UrlParameters is an empty collection
		/// </summary>
		[TestMethod]
		public void UrlParameters_Is_Empty()
		{
			Assert.IsNotNull(this.TestSubject.UrlParameters);
			Assert.AreEqual(0, this.TestSubject.UrlParameters.Count);
		}
	}
}
