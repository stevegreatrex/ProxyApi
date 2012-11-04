using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
			_engine.Execute("var jQuery = {};");
			_engine.Execute(_proxyGenerator.GenerateProxyScript());
		}

		private IProxyGenerator CreateProxyGenerator()
		{
			return new ProxyGenerator(
				new ControllerTypesProvider(new AppDomainAssemblyProvider()), 
				new ControllerDefinitionFactory(new ActionMethodsProvider(), new ActionMethodDefinitionFactory(new PassThroughPathUtility())));
		}

		#endregion

		#region Tests

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

		#region GET Tests

		/// <summary>
		/// Ensures that GetMethod calls jQuery.ajax w/ get
		/// </summary>
		[TestMethod]
		public void GetMethod_Calls_Ajax_For_Get_Without_Url_Parameters()
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
		public void GetMethod_Calls_Ajax_For_Get_With_Url_Parameters()
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
		public void PostMethod_Calls_Ajax_For_Post_Without_Data()
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
		public void PostMethod_Calls_Ajax_For_Post_With_Data()
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
		public void PutMethod_Calls_Ajax_For_Put_Without_Data()
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
		public void PutMethod_Calls_Ajax_For_Put_With_Url_Parameters()
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
		public void DeleteMethod_Calls_Ajax_For_Delete_Without_Url_Parameters()
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
		public void DeleteMethod_Calls_Ajax_For_Delete_With_Url_Parameters()
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

		#region Private Members

		private void SetupExpectedAjaxCall(string url, string type, object data = null, string returnData = null)
		{
			var script = 
@"jQuery.ajax = function(options) { 

	if (options.url ==='{url}' &&
		options.type ==='{type}') {
		
		var data = '{data}';
		if (data.length && JSON.stringify(options.data) !== '{data}') {
			throw 'Unexpected data: ' + JSON.stringify(options.data);
		}

		return {
			done: function(callback) {
				callback('{returnData}');
			}
		};
	}

	return {
		done: function() {
			throw 'No matching AJAX call setup: ' + JSON.stringify(options);
		}
	};
};"
			.Replace("{url}", url)
			.Replace("{type}", type)
			.Replace("{returnData}", returnData)
			.Replace("{data}", data == null ? "" : JsonConvert.SerializeObject(data));

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
		}

		#endregion
	}
}
