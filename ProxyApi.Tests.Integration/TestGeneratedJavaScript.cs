using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Jurassic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

		#endregion

		#region Private Members

		private void SetupExpectedAjaxCall(string url, string type, string returnData)
		{
			var script = 
@"jQuery.ajax = function(options) { 

	if (options.url ==='{url}' &&
		options.type ==='{type}') {
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
			.Replace("{returnData}", returnData);

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
