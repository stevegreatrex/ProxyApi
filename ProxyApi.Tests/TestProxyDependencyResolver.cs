using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="ProxyDependencyResolver"/> class.
	/// </summary>	
	[TestClass]
	public class TestProxyDependencyResolver : FixtureBase<ProxyDependencyResolver>
	{
		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ProxyDependencyResolver"/> for each
		/// unit test.
		/// </summary>
		public override ProxyDependencyResolver CreateTestSubject()
		{
			return new ProxyDependencyResolver();
		}

		#endregion

		#region Tests

		/// <summary>
		/// Ensures that GetService returns services for required classes
		/// </summary>
		[TestMethod]
		public void GetService_Returns_Services_For_Required_Classes()
		{
			CheckService(typeof(IProxyGenerator));
			CheckService(typeof(RouteHandler));
		}


		/// <summary>
		/// Ensures that Instance is not null
		/// </summary>
		[TestMethod]
		public void Instance_Is_Not_Null()
		{
			Assert.IsNotNull(ProxyDependencyResolver.Instance);
		}
		#endregion

		#region Private Members

		private void CheckService(Type contract)
		{
			var service = this.TestSubject.GetService(contract);
			Assert.IsNotNull(service, "The {0} service could not be instantiated", contract.Name);
			var services = this.TestSubject.GetServices(contract);
			Assert.AreEqual(1, services.Count(), "Only one instance of {0} should be available", contract.Name);
		}

		#endregion
	}
}
