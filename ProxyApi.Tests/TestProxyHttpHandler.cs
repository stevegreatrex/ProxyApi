using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProxyApi.Templates;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="ProxyHttpHandler"/> class.
	/// </summary>	
	[TestClass]
	public class TestProxyHttpHandler : FixtureBase<ProxyHttpHandler>
	{
		private Mock<IProxyGenerator> _generator;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ProxyHttpHandler"/> for each
		/// unit test.
		/// </summary>
		public override ProxyHttpHandler CreateTestSubject()
		{
			_generator = this.MockRepository.Create<IProxyGenerator>();

			return new ProxyHttpHandler(_generator.Object);
		}

		#endregion

		#region Tests

		/// <summary>
		/// Ensures that appropriate <see cref="ArgumentNullException"/> are thrown when
		/// null parameters are passed to the constructor.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Throws_Exception_On_Null_Parameters()
		{
			new ProxyHttpHandler(null);
		}

		/// <summary>
		/// Ensures that IsReusable returns true
		/// </summary>
		[TestMethod]
		public void IsReusable_Returns_True()
		{
			Assert.IsTrue(this.TestSubject.IsReusable);
		}

		/// <summary>
		/// Ensures that ProcessRequest throws an exception when passed a null context
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ProcessRequest_Throws_Exception_On_Null_Context()
		{
			this.TestSubject.ProcessRequest(null);
		}

		/// <summary>
		/// Ensures that ProcessRequest populates the context response
		/// </summary>
		[TestMethod]
		public void ProcessRequest_Populates_Context_Response()
		{
			var writer	= new StringWriter();
			var response = new HttpResponse(writer);
			var context	= new HttpContext(new HttpRequest("/","http://a","/"), response);

			//setup a call to the proxy generator
			_generator.Setup(g => g.GenerateProxyScript<JsProxyTemplate>()).Returns("generated script");

			this.TestSubject.ProcessRequest(context);

			Assert.AreEqual("generated script", writer.ToString());
			Assert.AreEqual("application/x-javascript", response.ContentType);

		}

		#endregion
	}
}
