using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyApi.Reflection;

namespace ProxyApi.Tests.Reflection
{
	/// <summary>
	/// Tests the functionality of the <see cref="AppDomainAssemblyProvider"/> class.
	/// </summary>	
	[TestClass]
	public class TestAppDomainAssemblyProvider : FixtureBase<AppDomainAssemblyProvider>
	{
		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="AppDomainAssemblyProvider"/> for each
		/// unit test.
		/// </summary>
		public override AppDomainAssemblyProvider CreateTestSubject()
		{
			return new AppDomainAssemblyProvider();
		}

		#endregion

		#region Tests

		/// <summary>
		/// Ensures that GetAssemblies returns AppDomain assemblies
		/// </summary>
		[TestMethod]
		public void GetAssemblies_Returns_AppDomain_Assemblies()
		{
			CollectionAssert.AreEqual(AppDomain.CurrentDomain.GetAssemblies().ToList(), this.TestSubject.GetAssemblies().ToList());
		}

		#endregion
	}
}
