using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ProxyApi.Tests.Reflection
{
	/// <summary>
	/// Tests the functionality of the <see cref="ControllerTypesProvider"/> class.
	/// </summary>	
	[TestClass]
	public class TestControllerTypesProvider : FixtureBase<ControllerTypesProvider>
	{
		private Mock<IAssemblyProvider> _assemblyProvider;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ControllerTypesProvider"/> for each
		/// unit test.
		/// </summary>
		public override ControllerTypesProvider CreateTestSubject()
		{
			_assemblyProvider = this.MockRepository.Create<IAssemblyProvider>();

			return new ControllerTypesProvider(_assemblyProvider.Object);
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
			new ControllerTypesProvider(null);
		}

		/// <summary>
		/// Ensures that GetControllerTypes correctly filters out inappropriate types
		/// </summary>
		[TestMethod]
		public void GetControllerTypes_Filters_Out_Inappropriate_Types()
		{
			//return some assemblies to make sure we get some hits and some things that should be filtered
			_assemblyProvider.Setup(p => p.GetAssemblies()).Returns(new [] {
				typeof(TestControllerTypesProvider).Assembly,
				typeof(System.Web.Http.ApiController).Assembly,
				typeof(System.Web.Mvc.Controller).Assembly });

			//get the list of types
			var controllerTypes = this.TestSubject.GetControllerTypes().ToList();

			//check that we only got the ones we expected
			Assert.AreEqual(2, controllerTypes.Count);
			Assert.AreEqual(typeof(SampleApiController), controllerTypes[0]);
			Assert.AreEqual(typeof(SampleMvcController), controllerTypes[1]);
		}

		#endregion

		#region Private Members



		#endregion
	}
}
