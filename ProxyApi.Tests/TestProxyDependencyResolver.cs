using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProxyApi.Reflection;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="ProxyDependencyResolver"/> class.
	/// </summary>	
	[TestClass]
	public class TestProxyDependencyResolver : FixtureBase<ProxyDependencyResolver>
	{
		private IProxyGeneratorConfiguration _configuration;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ProxyDependencyResolver"/> for each
		/// unit test.
		/// </summary>
		public override ProxyDependencyResolver CreateTestSubject()
		{
			_configuration = new ProxyGeneratorConfiguration();

			return new ProxyDependencyResolver(_configuration);
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
			new ProxyDependencyResolver(null);
		}

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
		/// Ensures that GetService returns PathUtility from configuration, if present
		/// </summary>
		[TestMethod]
		public void GetService_Returns_PathUtility_From_Configuration()
		{
			var pathUtility = new Mock<IPathUtility>().Object;
			_configuration.PathUtility = pathUtility;

			var resolved = this.TestSubject.GetService(typeof(IPathUtility));
			Assert.AreEqual(pathUtility, resolved, "Resolver should return element from configuration");

			_configuration.PathUtility = null;
			resolved = this.TestSubject.GetService(typeof(IPathUtility));
			Assert.IsNotNull(resolved, "Should still return valid item when configuration set to null");
			Assert.AreEqual(typeof(PathUtility), resolved.GetType());
		}

		/// <summary>
		/// Ensures that GetService returns AssemblyProvider from configuration, if present
		/// </summary>
		[TestMethod]
		public void GetService_Returns_AssemblyProvider_From_Configuration()
		{
			var assemblyProvider = new Mock<IAssemblyProvider>().Object;
			_configuration.AssemblyProvider = assemblyProvider;

			var resolved = this.TestSubject.GetService(typeof(IAssemblyProvider));
			Assert.AreEqual(assemblyProvider, resolved, "Resolver should return element from configuration");

			_configuration.AssemblyProvider = null;
			resolved = this.TestSubject.GetService(typeof(IAssemblyProvider));
			Assert.IsNotNull(resolved, "Should still return valid item when configuration set to null");
			Assert.AreEqual(typeof(AppDomainAssemblyProvider), resolved.GetType());
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
