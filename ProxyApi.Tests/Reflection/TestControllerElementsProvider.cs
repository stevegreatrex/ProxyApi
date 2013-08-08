using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProxyApi.Reflection;

namespace ProxyApi.Tests.Reflection
{
	/// <summary>
	/// Tests the functionality of the <see cref="ControllerElementsProvider"/> class.
	/// </summary>	
	[TestClass]
	public class TestControllerElementsProvider : FixtureBase<ControllerElementsProvider>
	{
		private Mock<IAssemblyProvider> _assemblyProvider;
		private IProxyGeneratorConfiguration _configuration;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ControllerElementsProvider"/> for each
		/// unit test.
		/// </summary>
		public override ControllerElementsProvider CreateTestSubject()
		{
			_assemblyProvider	= this.MockRepository.Create<IAssemblyProvider>();
			_configuration		= new ProxyGeneratorConfiguration();

			return new ControllerElementsProvider(_assemblyProvider.Object, _configuration);
		}

		#endregion

		#region Tests

		/// <summary>
		/// Ensures that appropriate <see cref="ArgumentNullException"/> are thrown when
		/// null parameters are passed to the constructor.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Throws_Exception_On_Null_AssemblyProvider()
		{
			new ControllerElementsProvider(null, _configuration);
		}

		
		/// <summary>
		/// Ensures that appropriate <see cref="ArgumentNullException"/> are thrown when
		/// null parameters are passed to the constructor.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Throws_Exception_On_Null_Configuration()
		{
			new ControllerElementsProvider(_assemblyProvider.Object, null);
		}

		/// <summary>
		/// Ensures that GetControllerTypes correctly filters out inappropriate types
		/// </summary>
		[TestMethod]
		public void GetControllerTypes_Filters_Out_Inappropriate_Types()
		{
			//return some assemblies to make sure we get some hits and some things that should be filtered
			_assemblyProvider.Setup(p => p.GetAssemblies()).Returns(new [] {
				typeof(TestControllerElementsProvider).Assembly,
				typeof(System.Web.Http.ApiController).Assembly,
				typeof(System.Web.Mvc.Controller).Assembly });

			//get the list of types
			var controllerTypes = this.TestSubject.GetControllerTypes().ToList();

			//check that we only got the ones we expected
			Assert.AreEqual(2, controllerTypes.Count);
			Assert.AreEqual(typeof(SampleApiController), controllerTypes[0]);
			Assert.AreEqual(typeof(SampleMvcController), controllerTypes[1]);
		}

		/// <summary>
		/// Ensures that GetControllerTypes ignores exceptions thrown from GetTypes
		/// </summary>
		[TestMethod]
		public void GetControllerTypes_Ignores_Exceptions_From_GetTypes()
		{
			_assemblyProvider.Setup(p => p.GetAssemblies()).Returns(new [] { new BadAssembly() });

			var controllerTypes = this.TestSubject.GetControllerTypes().ToList();
			//no exceptions so pass!
			Assert.AreEqual(0, controllerTypes.Count);
		}

		/// <summary>
		/// Ensures that GetControllerTypes returns an empty list when inclusion rule is set to exclude
		/// </summary>
		[TestMethod]
		public void GetControllerTypes_Returns_Explicit_Includes_List_For_Exclude_Rule()
		{
			_configuration.InclusionRule = InclusionRule.ExcludeAll;

			//return some assemblies to make sure we get some hits and some things that should be filtered
			_assemblyProvider.Setup(p => p.GetAssemblies()).Returns(new [] {
				typeof(TestControllerElementsProvider).Assembly,
				typeof(System.Web.Http.ApiController).Assembly,
				typeof(System.Web.Mvc.Controller).Assembly });

			//get the list of types
			var controllerTypes = this.TestSubject.GetControllerTypes().ToList();

			//check that the explicit includes were returned
			Assert.AreEqual(1, controllerTypes.Count);
			Assert.AreEqual(typeof(SampleApiController), controllerTypes[0], "Explicit includes should always be included");
		}

		/// <summary>
		/// Ensures that GetMethods throws an exception when passed a null type
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetMethods_Throws_Exception_On_Null_Type()
		{
			this.TestSubject.GetMethods(null).ToList();
		}

		/// <summary>
		/// Ensures that GetMethods returns only public instance methods
		/// </summary>
		[TestMethod]
		public void GetMethods_Returns_Only_Public_Instance_Methods()
		{
			var methods = this.TestSubject.GetMethods(typeof(Sample)).ToList();

			Assert.AreEqual(2, methods.Count);
			Assert.AreEqual("PublicMethod", methods[0].Name);
			Assert.AreEqual("OverloadedMethod", methods[1].Name);
		}

		/// <summary>
		/// Ensures that GetMethods returns overloaded method with most parameters
		/// </summary>
		[TestMethod]
		public void GetMethods_Returns_Overload_With_Most_Parameters()
		{
			var methods = this.TestSubject.GetMethods(typeof(Sample)).ToList();

			Assert.AreEqual("OverloadedMethod", methods[1].Name);
			Assert.AreEqual(2, methods[1].GetParameters().Count(), "GetMethods should select the overload with the most parameters");
		}

		/// <summary>
		/// Ensures that GetMethods treats named overloaded methods as different groups
		/// </summary>
		[TestMethod]
		public void GetMethods_Treats_Named_Overloaded_Methods_As_Separate_Groups()
		{
			var methods = this.TestSubject.GetMethods(typeof(SampleWithNamedOverloads)).ToList();

			Assert.AreEqual(2, methods.Count);
			Assert.AreEqual("OverloadedMethod", methods[0].Name);
			Assert.AreEqual("named", methods[1].GetCustomAttribute<ProxyNameAttribute>().Name);
			Assert.AreEqual("OtherMethod", methods[1].Name);
			Assert.AreEqual(3, methods[1].GetParameters().Count(), "GetMethods should select the named overload with the most parameters, regardless of actual name");
		}

		/// <summary>
		/// Ensures that GetMethods returns only implicit includes when the default inclusion rule is ExcludeAll
		/// </summary>
		[TestMethod]
		public void GetMethods_Returns_Explicit_Includes_When_InclusionRule_Is_Exclude()
		{
			_configuration.InclusionRule = InclusionRule.ExcludeAll;

			var methods = this.TestSubject.GetMethods(typeof(Sample)).ToList();

			Assert.AreEqual(1, methods.Count);
			Assert.AreEqual("OverloadedMethod", methods[0].Name, "Explicitly included methods should be included");
		}

		/// <summary>
		/// Ensures that GetMethods includes all methods in a type if the type has specified an inclusion rule
		/// </summary>
		[TestMethod]
		public void GetMethods_Includes_All_Methods_If_Type_Has_Explicit_Include()
		{
			_configuration.InclusionRule = InclusionRule.ExcludeAll;

			var methods = this.TestSubject.GetMethods(typeof(IncludedSampleWithExcludedMethods)).ToList();

			Assert.AreEqual(2, methods.Count);
			Assert.AreEqual("DefaultMethod", methods[0].Name, "Should fall back to the Type's rule attribute");
			Assert.AreEqual("IncludedMethod", methods[1].Name, "Explicitly included methods should be included");
		}

		#endregion

		#region Private Members

		class BadAssembly : Assembly
		{
			public override Type[] GetTypes()
			{
				throw new Exception("Oh no!");
			}
		}

		class SampleBase
		{
			public void InheritedPublicMethod()
			{}
		}

		class Sample : SampleBase
		{
			private void PrivateMethod()
			{}

			public void PublicMethod()
			{}

			public static void PublicStaticMethod()
			{}

			[ProxyInclude]
			public void OverloadedMethod()
			{}

			public void OverloadedMethod(object one)
			{}

			public void OverloadedMethod(object one, object two)
			{}

			[ProxyExclude]
			public void ExplicitlyExcluded()
			{}
		}

		class SampleWithNamedOverloads
		{
			public void OverloadedMethod()
			{}

			public void OverloadedMethod(object one)
			{}

			[ProxyName("named")]
			public void OverloadedMethod(string one)
			{}

			public void OverloadedMethod(object one, object two)
			{}

			[ProxyName("named")]
			public void OverloadedMethod(string one, string two)
			{}

			[ProxyName("named")]
			public void OtherMethod(string one, string two, string three)
			{}
		}

		[ProxyInclude]
		class IncludedSampleWithExcludedMethods
		{
			[ProxyExclude]
			public void ExcludedMethod()
			{}

			public void DefaultMethod()
			{}

			[ProxyInclude]
			public void IncludedMethod()
			{}
		}

		#endregion
	}
}
