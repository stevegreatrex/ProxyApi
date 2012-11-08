using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProxyApi.ElementDefinitions;
using ProxyApi.Reflection;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="ProxyGenerator"/> class.
	/// </summary>	
	[TestClass]
	public class TestProxyGenerator : FixtureBase<ProxyGenerator>
	{
		private Mock<IControllerElementsProvider> _typesProvider;
		private Mock<IControllerDefinitionFactory> _factory;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ProxyGenerator"/> for each
		/// unit test.
		/// </summary>
		public override ProxyGenerator CreateTestSubject()
		{
			_typesProvider	= this.MockRepository.Create<IControllerElementsProvider>();
			_factory		= this.MockRepository.Create<IControllerDefinitionFactory>();

			return new ProxyGenerator(_typesProvider.Object, _factory.Object);
		}

		#endregion

		#region Tests

		/// <summary>
		/// Ensures that appropriate <see cref="ArgumentNullException"/> are thrown when
		/// null parameters are passed to the constructor.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Throws_Exception_On_Null_TypesProvider()
		{
			new ProxyGenerator(null, _factory.Object);
		}

		/// <summary>
		/// Ensures that appropriate <see cref="ArgumentNullException"/> are thrown when
		/// null parameters are passed to the constructor.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Throws_Exception_On_Null_Factory()
		{
			new ProxyGenerator(_typesProvider.Object, null);
		}

		/// <summary>
		/// Ensures that GenerateProxyScript returns a generated template
		/// </summary>
		[TestMethod]
		public void GenerateProxyScript_Returns_Generated_Template()
		{
			//setup a call to the types
			var types = new [] {
				typeof(SampleApiController),
				typeof(SampleMvcController)
			};
			_typesProvider.Setup(t => t.GetControllerTypes()).Returns(types);

			//setup factory calls
			var definitions = new [] {
				new ControllerDefinition { Name = "controller1" },
				new ControllerDefinition { Name = "controller2" }
			};
			_factory.Setup(f => f.Create(types[0])).Returns(definitions[0]);
			_factory.Setup(f => f.Create(types[1])).Returns(definitions[1]);

			//can't test too much without making it fragile.  Just test the expected calls
			var script = this.TestSubject.GenerateProxyScript();
			Assert.IsFalse(string.IsNullOrEmpty(script), "The generated script should not be empty");
			Assert.IsTrue(script.Contains("controller1"), "The controllers should be included");
			Assert.IsTrue(script.Contains("controller2"), "The controllers should be included");

		}

		#endregion

		#region Private Members



		#endregion
	}
}
