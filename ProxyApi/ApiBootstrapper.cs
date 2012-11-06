using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Diagnostics.CodeAnalysis;

[assembly: PreApplicationStartMethod(typeof(ProxyApi.ApiBootstrapper), "RegisterProxyRoutes")]

namespace ProxyApi
{
	/// <summary>
	/// Bootstrapper that starts up the proxy generator.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class ApiBootstrapper
	{
		/// <summary>
		/// Sets up the proxy route table entries.
		/// </summary>
		public static void RegisterProxyRoutes()
		{
			var routeValues = new RouteValueDictionary();
			routeValues.Add("controller", null);
			routeValues.Add("action", null);

			RouteTable.Routes.Add("ProxyApi",
				new Route("api/proxies", routeValues, ProxyDependencyResolver.Instance.GetService<RouteHandler>()));

			GlobalConfiguration.Configuration.Routes.MapHttpRoute(
				name: "ApiProxy",
				routeTemplate: "api/{proxy}/{controller}/{action}",
				defaults: new {},
				constraints: new { proxy = "^proxy$" } //note: this is to prevent EVERY controller/action route being returned as api/proxy/controller/action when calling Url.Action etc
			);
		}
	}
}
