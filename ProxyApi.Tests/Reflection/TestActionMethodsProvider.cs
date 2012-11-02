using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyApi.Reflection;

namespace ProxyApi.Tests.Reflection
{
	/// <summary>
	/// Tests the functionality of the <see cref="ActionMethodsProvider"/> class.
	/// </summary>	
	[TestClass]
	public class TestActionMethodsProvider : FixtureBase<ActionMethodsProvider>
	{
		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ActionMethodsProvider"/> for each
		/// unit test.
		/// </summary>
		public override ActionMethodsProvider CreateTestSubject()
		{
			return new ActionMethodsProvider();
		}

		#endregion

		#region Tests

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

		#endregion

		#region Private Members

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

			public void OverloadedMethod()
			{}

			public void OverloadedMethod(object one)
			{}

			public void OverloadedMethod(object one, object two)
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

		#endregion
	}
}
