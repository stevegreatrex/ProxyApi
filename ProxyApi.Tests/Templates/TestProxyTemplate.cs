using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyApi.Templates;


namespace ProxyApi.Tests.Templates
{
	/// <summary>
	/// Tests the functionality of the <see cref="ProxyTemplate"/> class.
	/// </summary>	
	[TestClass]
	public class TestProxyTemplate : FixtureBase<JsProxyTemplate>
	{
		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ProxyTemplate"/> for each
		/// unit test.
		/// </summary>
		public override JsProxyTemplate CreateTestSubject()
		{
            return new JsProxyTemplate();
		}

		#endregion

		#region Tests

		/// <summary>
		/// Ensures that Definitions is an empty collection
		/// </summary>
		[TestMethod]
		public void Definitions_Is_Empty()
		{
			Assert.IsNotNull(this.TestSubject.Definitions);
			Assert.AreEqual(0, this.TestSubject.Definitions.Count());
		}

		#endregion
	}
}
