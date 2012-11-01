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
	/// Tests the functionality of the <see cref="AttributeExtensions"/> class.
	/// </summary>
	[TestClass]
	public class TestAttributeExtensions
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
		}
	}
}
