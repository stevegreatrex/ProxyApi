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

[assembly: PreApplicationStartMethod(typeof(ProxyApi.Bootstrapper), "Initialize")]

namespace ProxyApi
{
	/// <summary>
	/// Bootstrapper that starts up the proxy generator.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class Bootstrapper
	{
		/// <summary>
		/// Sets up routes for proxy resolution.
		/// </summary>
		public static void Initialize()
		{
			GlobalConfiguration.Configuration.Routes.MapHttpRoute(
				name: "ApiProxy",
				routeTemplate: "api/proxy/{controller}/{action}"
			);

			RouteTable.Routes.MapRoute(
				name: "DefaultProxy",
				url: "proxy/{controller}/{action}"
			);

			RouteTable.Routes.Add("ProxyApi",
				new Route("api/proxies", ProxyDependencyResolver.Instance.GetService<RouteHandler>()));
		}
	}
}
