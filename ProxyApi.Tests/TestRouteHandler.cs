using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="RouteHandler"/> class.
	/// </summary>	
	[TestClass]
	public class TestRouteHandler : FixtureBase<RouteHandler>
	{
		private Mock<IProxyGenerator> _proxyGenerator;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="RouteHandler"/> for each
		/// unit test.
		/// </summary>
		public override RouteHandler CreateTestSubject()
		{
			_proxyGenerator = this.MockRepository.Create<IProxyGenerator>();

			return new RouteHandler(_proxyGenerator.Object);
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
			new RouteHandler(null);
		}

		/// <summary>
		/// Ensures that GetHttpHandler throws on null request context
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetHttpHandler_Throws_Exception_On_Null_RequestContext()
		{
			this.TestSubject.GetHttpHandler(null);
		}

		/// <summary>
		/// Ensures that GetHttpHandler returns a valid handler
		/// </summary>
		[TestMethod]
		public void GetHttpHandler_Returns_Valid_Handler()
		{
			Assert.IsNotNull(this.TestSubject.GetHttpHandler(new RequestContext()));
		}

		#endregion
	}
}
