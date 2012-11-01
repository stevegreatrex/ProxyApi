using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProxyApi.ElementDefinitions;
using ProxyApi.Factories;
using ProxyApi.Reflection;

namespace ProxyApi.Tests.Factories
{
	/// <summary>
	/// Tests the functionality of the <see cref="ControllerDefinitionFactory"/> class.
	/// </summary>	
	[TestClass]
	public class TestControllerDefinitionFactory : FixtureBase<ControllerDefinitionFactory>
	{
		private Mock<IActionMethodsProvider> _actionProvider;
		private Mock<IActionMethodDefinitionFactory> _actionFactory;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ControllerDefinitionFactory"/> for each
		/// unit test.
		/// </summary>
		public override ControllerDefinitionFactory CreateTestSubject()
		{
			_actionProvider = this.MockRepository.Create<IActionMethodsProvider>();
			_actionFactory = this.MockRepository.Create<IActionMethodDefinitionFactory>();

			return new ControllerDefinitionFactory(_actionProvider.Object, _actionFactory.Object);
		}

		#endregion

		#region Tests

		/// <summary>
		/// Ensures that appropriate <see cref="ArgumentNullException"/> are thrown when
		/// null parameters are passed to the constructor.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Throws_Exception_On_Null_ActionProvider()
		{
			new ControllerDefinitionFactory(null, _actionFactory.Object);
		}

		/// <summary>
		/// Ensures that appropriate <see cref="ArgumentNullException"/> are thrown when
		/// null parameters are passed to the constructor.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Throws_Exception_On_Null_ActionFactory()
		{
			new ControllerDefinitionFactory(_actionProvider.Object, null);
		}

		/// <summary>
		/// Ensures that Create throws an exception when passed a null type
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_Throws_Exception_On_Null_Type()
		{
			this.TestSubject.Create(null);
		}

		/// <summary>
		/// Ensures that Create sets definition name
		/// </summary>
		[TestMethod]
		public void Create_Sets_Definition_Name()
		{
			_actionProvider.Setup(ap => ap.GetMethods(It.IsAny<Type>()))
				.Returns(Enumerable.Empty<MethodInfo>());

			var definition = this.TestSubject.Create(typeof(SampleApiController));
			Assert.AreEqual("sampleapi", definition.Name);
		}

		/// <summary>
		/// Ensures that Create sets definition type
		/// </summary>
		[TestMethod]
		public void Create_Sets_Definition_Type()
		{
			_actionProvider.Setup(ap => ap.GetMethods(It.IsAny<Type>()))
				.Returns(Enumerable.Empty<MethodInfo>());

			var apiDefinition = this.TestSubject.Create(typeof(SampleApiController));
			var mvcDefinition = this.TestSubject.Create(typeof(SampleMvcController));

			Assert.AreEqual(ControllerType.WebApi, apiDefinition.Type);
			Assert.AreEqual(ControllerType.Mvc, mvcDefinition.Type);
		}

		/// <summary>
		/// Ensures that Create populates action methods
		/// </summary>
		[TestMethod]
		public void Create_Populates_Action_Methods()
		{
			var type	= typeof(SampleApiController);
			var methods	= new [] {
				type.GetMethod("Method1"),
				type.GetMethod("Method2"),
				type.GetMethod("Method3")
			};

			//setup the expected call to the method provider
			_actionProvider.Setup(ap => ap.GetMethods(type)).Returns(methods);

			//setup the expected calls to the factory
			var methodDefinitions = new [] {
				new ActionMethodDefinition(),
				new ActionMethodDefinition(),
				new ActionMethodDefinition()
			};
			_actionFactory.Setup(af => af.Create(It.IsAny<IControllerDefinition>(), methods[0])).Returns(methodDefinitions[0]);
			_actionFactory.Setup(af => af.Create(It.IsAny<IControllerDefinition>(), methods[1])).Returns(methodDefinitions[1]);
			_actionFactory.Setup(af => af.Create(It.IsAny<IControllerDefinition>(), methods[2])).Returns(methodDefinitions[2]);

			//create the definition
			var definition = this.TestSubject.Create(type);

			//check the created definition
			CollectionAssert.AreEqual(
				methodDefinitions.ToList(),
				definition.ActionMethods.ToList());
		}

		#endregion
	}
}
