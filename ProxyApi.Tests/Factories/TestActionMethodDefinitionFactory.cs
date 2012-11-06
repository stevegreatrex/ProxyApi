using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProxyApi.ElementDefinitions;
using ProxyApi.Factories;

namespace ProxyApi.Tests.Factories
{
	/// <summary>
	/// Tests the functionality of the <see cref="ActionMethodDefinitionFactory"/> class.
	/// </summary>	
	[TestClass]
	public class TestActionMethodDefinitionFactory : FixtureBase<ActionMethodDefinitionFactory>
	{
		private Mock<IPathUtility> _pathUtility;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ActionMethodDefinitionFactory"/> for each
		/// unit test.
		/// </summary>
		public override ActionMethodDefinitionFactory CreateTestSubject()
		{
			_pathUtility = this.MockRepository.Create<IPathUtility>();

			return new ActionMethodDefinitionFactory(_pathUtility.Object);
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
			new ActionMethodDefinitionFactory(null);
		}

		/// <summary>
		/// Ensures that Create throws exception on null controller definition
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_Throws_Exception_On_Null_ControllerDefinition()
		{
			var methodInfo = typeof(TestActionMethodDefinitionFactory).GetMethod("Create_Throws_Exception_On_Null_ControllerDefinition");
			this.TestSubject.Create(null, methodInfo);
		}

		/// <summary>
		/// Ensures that Create throws exception on null method info
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_Throws_Exception_On_Null_MethodInfo()
		{
			this.TestSubject.Create(new ControllerDefinition(), null);
		}

		/// <summary>
		/// Ensures that Create sets method name
		/// </summary>
		[TestMethod]
		public void Create_Sets_Method_Name()
		{
			SetupPathUtility("NamedMethod", "controller");

			var method		= GetMethodInfo("NamedMethod");
			var definition	= this.TestSubject.Create(new ControllerDefinition { UrlName = "controller" }, method);

			Assert.AreEqual("userSpecifiedName", definition.Name);
		}

		/// <summary>
		/// Ensures that Create sets URL
		/// </summary>
		[TestMethod]
		public void Create_Sets_URL_For_Api_Controllers()
		{
			_pathUtility.Setup(p => p.ToAbsolute("~/api/proxy/controller/methodname"))
				.Returns("/url");

			var method		= GetMethodInfo("MethodName");
			var definition	= this.TestSubject.Create(new ControllerDefinition() {
				UrlName = "controller",
				Type = ControllerType.WebApi
			}, method);

			Assert.AreEqual("/url", definition.Url, "The URL should be populated");
		}

		/// <summary>
		/// Ensures that Create sets URL
		/// </summary>
		[TestMethod]
		public void Create_Sets_URL_For_Mvc_Controllers()
		{
			SetupPathUtility("MethodName", "controller");

			var method		= GetMethodInfo("MethodName");
			var definition	= this.TestSubject.Create(new ControllerDefinition() {
				UrlName = "controller",
				Type = ControllerType.Mvc
			}, method);

			Assert.AreEqual("/url", definition.Url, "The URL should be populated");
		}

		/// <summary>
		/// Ensures that Create sets the correct method type
		/// </summary>
		[TestMethod]
		public void Create_Sets_Correct_Type()
		{
			CheckMethodType("MethodName", HttpVerbs.Get);
			CheckMethodType("GetData", HttpVerbs.Get);
			CheckMethodType("WebGet", HttpVerbs.Get);
			CheckMethodType("MvcGet", HttpVerbs.Get);
			CheckMethodType("WebAcceptVerbsGet", HttpVerbs.Get);
			CheckMethodType("MvcAcceptVerbsGet", HttpVerbs.Get);

			CheckMethodType("PostData", HttpVerbs.Post);
			CheckMethodType("WebPost", HttpVerbs.Post);
			CheckMethodType("MvcPost", HttpVerbs.Post);
			CheckMethodType("WebAcceptVerbsPost", HttpVerbs.Post);
			CheckMethodType("MvcAcceptVerbsPost", HttpVerbs.Post);

			CheckMethodType("PutData", HttpVerbs.Put);
			CheckMethodType("WebPut", HttpVerbs.Put);
			CheckMethodType("MvcPut", HttpVerbs.Put);
			CheckMethodType("WebAcceptVerbsPut", HttpVerbs.Put);
			CheckMethodType("MvcAcceptVerbsPut", HttpVerbs.Put);

			CheckMethodType("DeleteData", HttpVerbs.Delete);
			CheckMethodType("WebDelete", HttpVerbs.Delete);
			CheckMethodType("MvcDelete", HttpVerbs.Delete);
			CheckMethodType("WebAcceptVerbsDelete", HttpVerbs.Delete);
			CheckMethodType("MvcAcceptVerbsDelete", HttpVerbs.Delete);
		}

		/// <summary>
		/// Ensures that Create populates parameters correctly
		/// </summary>
		[TestMethod]
		public void Create_Populates_Parameters_Correctly()
		{
			CheckMethodParameters("GetWithUrlParameters", null, "one", "two", "three");
			
			CheckMethodParameters("PostWithImpliedBody", "one");
			CheckMethodParameters("PostWithExplicitBody", "one");
			CheckMethodParameters("PostWithExplicitBodyAndUrlParameters", "two", "one");
			CheckMethodParameters("PostWithUrlParameters", null, "one", "two");

			CheckMethodParameters("PutWithExplicitBody", "one");
			CheckMethodParameters("PutWithExplicitBodyAndUrlParameters", "two", "one");
			CheckMethodParameters("PutWithUrlParameters", null, "one", "two");
			
			CheckMethodParameters("DeleteWithExplicitBody", "one");
			CheckMethodParameters("DeleteWithExplicitBodyAndUrlParameters", "two", "one");
			CheckMethodParameters("DeleteWithUrlParameters", null, "one", "two");
		}

		#endregion

		#region Private Members

		[ProxyName("otherControllerName")] //this is included to check that the proxy name is NOT used in URLs
		class Sample
		{
			#region Type Checks

			[ProxyName("otherMethodName")] //this is included to check that the proxy name is NOT used in URLs
			public void MethodName() {}
			public void GetData(){}
			[System.Web.Http.HttpGet] public void WebGet() {}
			[System.Web.Mvc.HttpGet] public void MvcGet() {}
			[System.Web.Http.AcceptVerbs("Get")] public void WebAcceptVerbsGet() {}
			[System.Web.Mvc.AcceptVerbs(HttpVerbs.Get)] public void MvcAcceptVerbsGet() {}

			public void PostData(){}
			[System.Web.Http.HttpPost] public void WebPost() {}
			[System.Web.Mvc.HttpPost] public void MvcPost() {}
			[System.Web.Http.AcceptVerbs("Post")] public void WebAcceptVerbsPost() {}
			[System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)] public void MvcAcceptVerbsPost() {}

			public void PutData(){}
			[System.Web.Http.HttpPut] public void WebPut() {}
			[System.Web.Mvc.HttpPut] public void MvcPut() {}
			[System.Web.Http.AcceptVerbs("Put")] public void WebAcceptVerbsPut() {}
			[System.Web.Mvc.AcceptVerbs(HttpVerbs.Put)] public void MvcAcceptVerbsPut() {}

			public void DeleteData(){}
			[System.Web.Http.HttpDelete] public void WebDelete() {}
			[System.Web.Mvc.HttpDelete] public void MvcDelete() {}
			[System.Web.Http.AcceptVerbs("Delete")] public void WebAcceptVerbsDelete() {}
			[System.Web.Mvc.AcceptVerbs(HttpVerbs.Delete)] public void MvcAcceptVerbsDelete() {}

			#endregion

			#region Parameter Checks

			public void GetWithUrlParameters(object one, object two, object three) {}
			
			public void PostWithImpliedBody(object one){}
			public void PostWithExplicitBody([FromBody]object one){}
			public void PostWithExplicitBodyAndUrlParameters(object one, [FromBody]object two){}
			public void PostWithUrlParameters(object one, object two){}

			public void PutWithExplicitBody([FromBody]object one){}
			public void PutWithExplicitBodyAndUrlParameters(object one, [FromBody]object two){}
			public void PutWithUrlParameters(object one, object two){}

			public void DeleteWithExplicitBody([FromBody]object one){}
			public void DeleteWithExplicitBodyAndUrlParameters(object one, [FromBody]object two){}
			public void DeleteWithUrlParameters(object one, object two){ }

			#endregion

			#region Name Checks

			[ProxyName("userSpecifiedName")]
			public void NamedMethod(){}

			#endregion
		}

		private static MethodInfo GetMethodInfo(string name)
		{
			return typeof(Sample).GetMethod(name);
		}

		private void CheckMethodType(string methodName, HttpVerbs type)
		{
			SetupPathUtility(methodName, "controller");

			var definition = this.TestSubject.Create(new ControllerDefinition{ UrlName = "controller" }, GetMethodInfo(methodName));

			Assert.AreEqual(definition.Type, type, "Type for {0} was incorrect", methodName);
		}

		private void SetupPathUtility(string methodName, string controllerName)
		{
			_pathUtility.Setup(p => p.ToAbsolute("~/url"))
						 .Returns("/url");
			_pathUtility.Setup(p => p.GetVirtualPath(It.IsAny<RouteValueDictionary>()))
				.Callback<RouteValueDictionary>(rvd =>
					Assert.IsTrue(
						controllerName.Equals(rvd["controller"]) &&
						methodName.ToLower().Equals(rvd["action"])))
				.Returns("~/url");
		}

		private void CheckMethodParameters(string methodName, string bodyParameterName, params string[] urlParameters)
		{
			SetupPathUtility(methodName, "controller");

			var definition = this.TestSubject.Create(new ControllerDefinition { UrlName = "controller" }, GetMethodInfo(methodName));

			if (bodyParameterName == null)
				Assert.IsNull(definition.BodyParameter, "BodyParameter should be null");
			else
				Assert.AreEqual(definition.BodyParameter.Name, bodyParameterName, "BodyParameter should be set");

			Assert.AreEqual(definition.UrlParameters.Count, urlParameters.Length, "Unexpected number of URL parameters");
			for (int i = 0; i < urlParameters.Length; i++)
			{
				Assert.AreEqual(definition.UrlParameters[i].Name, urlParameters[i]);
			}
			
		}

		#endregion
	}
}
