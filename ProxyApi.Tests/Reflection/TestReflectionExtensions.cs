using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Web.Http;
using System.Reflection;
using ProxyApi.Reflection;

namespace ProxyApi.Tests.Reflection
{
	/// <summary>
	/// Tests the functionality of the <see cref="ReflectionExtensions"/> class.
	/// </summary>
	[TestClass]
	public class TestReflectionExtensions
	{
		private MethodInfo _withDescriptionAttribute;
		private MethodInfo _withBrowseableAttribute;
		private ParameterInfo _withFromBodyAttribute;
		private ParameterInfo _withoutFromBodyAttribute;

		[TestInitialize]
		public void GetReflectedInfo()
		{
			var sample = typeof(Sample);
			_withDescriptionAttribute = sample.GetMethod("WithDescriptionAttribute");
			_withBrowseableAttribute = sample.GetMethod("WithBrowseableAttribute");

			var parameters = sample.GetMethod("Parameters").GetParameters();
			_withFromBodyAttribute = parameters[0];
			_withoutFromBodyAttribute = parameters[1];
		}

		/// <summary>
		/// Ensures that GetCustomAttribute throws an exception when called on a null member
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetCustomAttribute_Throws_On_Null_Method()
		{
			MethodInfo target = null;
			target.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();
		}

		/// <summary>
		/// Ensures that GetCustomAttribute returns the expected attribute when present
		/// </summary>
		[TestMethod]
		public void GetCustomAttribute_Returns_Attribute_When_Present_On_Method()
		{
			var description = _withDescriptionAttribute.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();
			var blank		= _withBrowseableAttribute.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();

			Assert.IsNotNull(description);
			Assert.IsNull(blank);
		}

		/// <summary>
		/// Ensures that GetCustomAttribute throws an exception when called on a null member
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetCustomAttribute_Throws_On_Null_Parameter()
		{
			ParameterInfo target = null;
			target.GetCustomAttribute<FromBodyAttribute>();
		}

		/// <summary>
		/// Ensures that GetCustomAttribute returns the expected attribute when present
		/// </summary>
		[TestMethod]
		public void GetCustomAttribute_Returns_Attribute_When_Present_On_Parameter()
		{
			var fromBody	= _withFromBodyAttribute.GetCustomAttribute<FromBodyAttribute>();
			var blank		= _withoutFromBodyAttribute.GetCustomAttribute<FromBodyAttribute>();

			Assert.IsNotNull(fromBody);
			Assert.IsNull(blank);
		}

		/// <summary>
		/// Ensures that HasAttribute throws an exception when called on a null member
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void HasAttribute_Throws_On_Null_Method()
		{
			MethodInfo target = null;
			target.HasAttribute<System.ComponentModel.DescriptionAttribute>();
		}

		/// <summary>
		/// Ensures that HasAttribute returns the expected attribute when present
		/// </summary>
		[TestMethod]
		public void HasAttribute_Returns_Attribute_When_Present_On_Method()
		{
			var description = _withDescriptionAttribute.HasAttribute<System.ComponentModel.DescriptionAttribute>();
			var blank		= _withBrowseableAttribute.HasAttribute<System.ComponentModel.DescriptionAttribute>();

			Assert.IsTrue(description);
			Assert.IsFalse(blank);
		}

		/// <summary>
		/// Ensures that HasAttribute throws an exception when called on a null member
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void HasAttribute_Throws_On_Null_Parameter()
		{
			ParameterInfo target = null;
			target.HasAttribute<FromBodyAttribute>();
		}

		/// <summary>
		/// Ensures that HasAttribute returns the expected attribute when present
		/// </summary>
		[TestMethod]
		public void HasAttribute_Returns_Attribute_When_Present_On_Parameter()
		{
			var fromBody	= _withFromBodyAttribute.HasAttribute<FromBodyAttribute>();
			var blank		= _withoutFromBodyAttribute.HasAttribute<FromBodyAttribute>();

			Assert.IsTrue(fromBody);
			Assert.IsFalse(blank);
		}

		/// <summary>
		/// Ensures that GetProxyName throws an exception when called on a null member
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetProxyName_Throws_On_Null_Method()
		{
			MethodInfo target = null;
			target.GetProxyName();
		}

		/// <summary>
		/// Ensures that GetProxyName throws an exception when called on a null type
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetProxyName_Throws_On_Null_Type()
		{
			Type target = null;
			target.GetProxyName();
		}

		/// <summary>
		/// Ensures that GetProxyName returns the method name to lower case when no attribute is specified
		/// </summary>
		[TestMethod]
		public void GetProxyName_Returns_Method_Name_When_No_Attribute_Present()
		{
			Assert.AreEqual("withdescriptionattribute", _withDescriptionAttribute.GetProxyName());
			Assert.AreEqual("withbrowseableattribute", _withBrowseableAttribute.GetProxyName());
		}

		/// <summary>
		/// Ensures that GetProxyName returns specified name when name attribute is present
		/// </summary>
		[TestMethod]
		public void GetProxyName_Returns_Specified_Name_When_NameAttribute_Present()
		{
			Assert.AreEqual("anUnexpectedName", typeof(Sample).GetMethod("NamedMethod").GetProxyName());
		}

		/// <summary>
		/// Ensures that GetProxyName returns the type name to lower case when no attribute is specified.
		/// Also checks removal of word "controller".
		/// </summary>
		[TestMethod]
		public void GetProxyName_Returns_Type_Name_When_No_Attribute_Present()
		{
			Assert.AreEqual("sample", typeof(Sample).GetProxyName());
			Assert.AreEqual("sampleapi", typeof(SampleApiController).GetProxyName());
		}

		/// <summary>
		/// Ensures that GetProxyName returns specified name when name attribute is present
		/// </summary>
		[TestMethod]
		public void GetProxyName_Returns_Specified_Name_When_NameAttribute_Present_On_Type()
		{
			Assert.AreEqual("explicitName", typeof(NamedSample).GetProxyName());
		}

		class Sample
		{
			[System.ComponentModel.Description]
			public void WithDescriptionAttribute()
			{}

			[System.ComponentModel.Browsable(false)]
			public void WithBrowseableAttribute()
			{}

			public void Parameters([FromBody]object withFromBody, object withoutFromBody)
			{}

			[ProxyName("anUnexpectedName")]
			public void NamedMethod()
			{}
		}

		[ProxyName("explicitName")]
		class NamedSample
		{
		}
	}
}
