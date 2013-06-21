using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Jurassic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using ProxyApi.Factories;
using ProxyApi.Reflection;

namespace ProxyApi.Tests.Integration
{
	[TestClass]
	public class TestGeneratedJavaScript
	{
		private ScriptEngine _engine;
		private IProxyGenerator _proxyGenerator;

		#region Setup

		[TestInitialize]
		public void Setup()
		{
			_proxyGenerator = CreateProxyGenerator();
			_engine			= new ScriptEngine();
			//create fake implementations of $.isFunction and $.extend
			_engine.Execute(@"var jQuery = { 
				isFunction: function() {}, 
				extend: function() {
					for(var i=1; i<arguments.length; i++)
						for(var key in arguments[i])
							if(arguments[i].hasOwnProperty(key))
								arguments[0][key] = arguments[i][key];
					return arguments[0]; 
				}
			};");
			_engine.Execute(_proxyGenerator.GenerateProxyScript());
		}

		private IProxyGenerator CreateProxyGenerator()
		{
			var controllerElementsProvider = new ControllerElementsProvider(
					new AppDomainAssemblyProvider(),
					ProxyGeneratorConfiguration.Default);
			return new ProxyGenerator(
				controllerElementsProvider, 
				new ControllerDefinitionFactory(
						controllerElementsProvider,
						new ActionMethodDefinitionFactory(new PassThroughPathUtility())));
		}

		#endregion

		#region Load Tests

		/// <summary>
		/// Ensures that ScriptLoad throws an error if jQuery is not defined
		/// </summary>
		[TestMethod]
		public void ScriptLoad_Throws_Error_If_jQuery_Not_Defined()
		{
			var engine			= new ScriptEngine();
			var proxyGenerator	= CreateProxyGenerator();
			try
			{
				engine.Execute(proxyGenerator.GenerateProxyScript());
				Assert.Fail("The javascript should throw an error if jQuery is not defined");
			}
			catch(JavaScriptException ex)
			{
				Assert.IsTrue(ex.Message.Contains("jQuery is not defined"));
			}
		}

		/// <summary>
		/// Ensures that ScriptLoad creates the $.proxies object
		/// </summary>
		[TestMethod]
		public void ScriptLoad_Creates_Proxies_Object()
		{
			var proxies = _engine.Evaluate("jQuery.proxies");
			Assert.IsNotNull(proxies, "The $.proxies object should have been created");

			var integrationTestApi = _engine.Evaluate("jQuery.proxies.apiIntegrationTest");
			Assert.IsNotNull(integrationTestApi);
		}

		#endregion

		#region Web API

		#region GET Tests

		/// <summary>
		/// Ensures that GetMethod calls jQuery.ajax w/ get
		/// </summary>
		[TestMethod]
		public void WebApi_GetMethod_Calls_Ajax_For_Get_Without_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/getdata",
				type: "get",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.getdata()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that GetMethod calls jQuery.ajax w/ get
		/// </summary>
		[TestMethod]
		public void WebApi_GetMethod_Calls_Ajax_For_Get_With_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/getdata?param1=one&param2=2&param3=true",
				type: "get",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.getdata('one',2,true)");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#region POST Tests

		/// <summary>
		/// Ensures that PostMethod calls jQuery.ajax with 'post'
		/// </summary>
		[TestMethod]
		public void WebApi_PostMethod_Calls_Ajax_For_Post_Without_Data()
		{
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/senddata",
				type: "post",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.senddata()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that PostMethod calls jQuery.ajax with 'post'
		/// </summary>
		[TestMethod]
		public void WebApi_PostMethod_Calls_Ajax_For_Post_With_Data()
		{
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/senddata",
				type: "post",
				data: new { one="one", two="two" },
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.senddata({ one: 'one', two: 'two' })");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#region PUT Tests

		/// <summary>
		/// Ensures that PutMethod calls jQuery.ajax with 'Put'
		/// </summary>
		[TestMethod]
		public void WebApi_PutMethod_Calls_Ajax_For_Put_Without_Data()
		{
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/senddatawithput",
				type: "put",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.putData()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that PutMethod calls jQuery.ajax with 'Put'
		/// </summary>
		[TestMethod]
		public void WebApi_PutMethod_Calls_Ajax_For_Put_With_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/senddatawithput?id=123",
				type: "put",
				data: new { one="one", two="two" },
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.putData(123, { one: 'one', two: 'two' })");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#region DELETE Tests

		/// <summary>
		/// Ensures that DeleteMethod calls jQuery.ajax with 'Delete'
		/// </summary>
		[TestMethod]
		public void WebApi_DeleteMethod_Calls_Ajax_For_Delete_Without_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/delete",
				type: "delete",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.delete()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that DeleteMethod calls jQuery.ajax with 'Delete'
		/// </summary>
		[TestMethod]
		public void WebApi_DeleteMethod_Calls_Ajax_For_Delete_With_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/delete?id=123",
				type: "delete",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.delete(123)");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#endregion

		#region MVC

		#region GET Tests

		/// <summary>
		/// Ensures that GetMethod calls jQuery.ajax w/ get
		/// </summary>
		[TestMethod]
		public void Mvc_GetMethod_Calls_Ajax_For_Get_Without_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/proxy/integrationtestmvc/getdata",
				type: "get",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.integrationtestmvc.getdata()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that GetMethod calls jQuery.ajax w/ get
		/// </summary>
		[TestMethod]
		public void Mvc_GetMethod_Calls_Ajax_For_Get_With_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/proxy/integrationtestmvc/getdata?param1=one&param2=2&param3=true",
				type: "get",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.integrationtestmvc.getdata('one',2,true)");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#region POST Tests

		/// <summary>
		/// Ensures that PostMethod calls jQuery.ajax with 'post'
		/// </summary>
		[TestMethod]
		public void Mvc_PostMethod_Calls_Ajax_For_Post_Without_Data()
		{
			SetupExpectedAjaxCall(
				url: "~/proxy/integrationtestmvc/senddata",
				type: "post",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.integrationtestmvc.senddata()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that PostMethod calls jQuery.ajax with 'post'
		/// </summary>
		[TestMethod]
		public void Mvc_PostMethod_Calls_Ajax_For_Post_With_Data()
		{
			SetupExpectedAjaxCall(
				url: "~/proxy/integrationtestmvc/senddata",
				type: "post",
				data: new { one="one", two="two" },
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.integrationtestmvc.senddata({ one: 'one', two: 'two' })");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#region PUT Tests

		/// <summary>
		/// Ensures that PutMethod calls jQuery.ajax with 'Put'
		/// </summary>
		[TestMethod]
		public void Mvc_PutMethod_Calls_Ajax_For_Put_Without_Data()
		{
			SetupExpectedAjaxCall(
				url: "~/proxy/integrationtestmvc/senddatawithput",
				type: "put",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.integrationtestmvc.putData()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that PutMethod calls jQuery.ajax with 'Put'
		/// </summary>
		[TestMethod]
		public void Mvc_PutMethod_Calls_Ajax_For_Put_With_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/proxy/integrationtestmvc/senddatawithput?id=123",
				type: "put",
				data: new { one="one", two="two" },
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.integrationtestmvc.putData(123, { one: 'one', two: 'two' })");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#region DELETE Tests

		/// <summary>
		/// Ensures that DeleteMethod calls jQuery.ajax with 'Delete'
		/// </summary>
		[TestMethod]
		public void Mvc_DeleteMethod_Calls_Ajax_For_Delete_Without_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/proxy/integrationtestmvc/delete",
				type: "delete",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.integrationtestmvc.delete()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that DeleteMethod calls jQuery.ajax with 'Delete'
		/// </summary>
		[TestMethod]
		public void Mvc_DeleteMethod_Calls_Ajax_For_Delete_With_Url_Parameters()
		{
			SetupExpectedAjaxCall(
				url: "~/proxy/integrationtestmvc/delete?id=123",
				type: "delete",
				returnData: "test data");

			var result = ExecuteProxyMethod("jQuery.proxies.integrationtestmvc.delete(123)");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#endregion

		#region AntiForgeryToken

		/// <summary>
		/// Ensures that AntiForgeryToken is set on the ajax request
		/// </summary>
		[TestMethod]
		public void AntiForgeryToken_Is_Set_On_Ajax_Request_From_Property()
		{
			_engine.Execute("jQuery.proxies.apiIntegrationTest.antiForgeryToken='anti-forgery';");

			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/getdata",
				type: "get",
				returnData: "test data",
				antiForgeryToken: "anti-forgery");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.getdata()");
			Assert.AreEqual("test data", result);
		}

		/// <summary>
		/// Ensures that AntiForgeryToken is set on the ajax request
		/// </summary>
		[TestMethod]
		public void AntiForgeryToken_Is_Set_On_Ajax_Request_From_Function()
		{
			_engine.Execute("jQuery.proxies.apiIntegrationTest.antiForgeryToken=function() { return 'anti-forgery'; };");
			_engine.Execute("jQuery.isFunction = function() { return true; };");

			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/getdata",
				type: "get",
				returnData: "test data",
				antiForgeryToken: "anti-forgery");

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.getdata()");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#region DefaultOptions

		/// <summary>
		/// Ensures that DefaultOptions are set on the ajax request
		/// </summary>
		[TestMethod]
		public void DefaultOptions_Are_Set_On_Ajax_Request()
		{
			_engine.Execute("jQuery.proxies.apiIntegrationTest.defaultOptions.customOption = 'custom';");
			_engine.Execute("jQuery.proxies.apiIntegrationTest.defaultOptions.type = 'custom';"); //will be overwritten
			
			SetupExpectedAjaxCall(
				url: "~/api/proxy/integrationtestapi/getdata",
				type: "get",
				returnData: "test data",
				defaultOptions: new {
					customOption = "custom"
				});

			var result = ExecuteProxyMethod("jQuery.proxies.apiIntegrationTest.getdata()");
			Assert.AreEqual("test data", result);
		}

		#endregion

		#region Private Members

		private void SetupExpectedAjaxCall(string url, 
			string type, 
			object data = null, 
			string returnData = null,
			string antiForgeryToken = null, 
			object defaultOptions = null)
		{
			var script = 
@"jQuery.ajax = function(options) { 

	if (options.url ==='{url}' &&
		options.type ==='{type}') {

		if ('{antiForgeryToken}') {
			if (!options.headers ||
				!options.headers['X-RequestVerificationToken'] ||
				options.headers['X-RequestVerificationToken'] !== '{antiForgeryToken}') {

				throw 'Expected anti-forgery token: ' + JSON.stringify(options, null, 2);
			}
		}

		var defaultOptions = {defaultOptions};
		if (defaultOptions) {
			for (var prop in defaultOptions) {
				if (options[prop] !== defaultOptions[prop]) {
					throw 'Default options were not set: ' + JSON.stringify(options, null, 2);
				}
			}
		}
		
		var data = '{data}';
		if (data.length && JSON.stringify(options.data) !== '{data}') {
			throw 'Unexpected data: ' + JSON.stringify(options.data, null, 2);
		}

		return {
			done: function(callback) {
				callback('{returnData}');
			}
		};
	}

	return {
		done: function() {
			throw 'No matching AJAX call setup: ' + JSON.stringify(options, null, 2);
		}
	};
};"
			.Replace("{url}", url)
			.Replace("{type}", type)
			.Replace("{returnData}", returnData)
			.Replace("{data}", data == null ? "" : JsonConvert.SerializeObject(data))
			.Replace("{antiForgeryToken}", antiForgeryToken)
			.Replace("{defaultOptions}", defaultOptions == null ? "null"  : JsonConvert.SerializeObject(data));

			_engine.Execute(script);
		}

		private object ExecuteProxyMethod(string method)
		{
			var result = _engine.Evaluate(@"(function() {
  var result;
  {method}
    .done(function(data) {
      result = data;
    });
  return result;
})()".Replace("{method}", method));
			return result;
		}

		class PassThroughPathUtility : IPathUtility
		{
			public string ToAbsolute(string path)
			{
				return path;
			}

			public string GetVirtualPath(RouteValueDictionary routeValues)
			{
				return string.Format("~/proxy/{0}/{1}", routeValues["controller"], routeValues["action"]);
			}
		}

		#endregion
	}
}
