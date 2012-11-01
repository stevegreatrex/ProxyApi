using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

[assembly: PreApplicationStartMethod(typeof(ProxyApi.Bootstrapper), "Initialize")]

namespace ProxyApi
{
	public static class Bootstrapper
	{
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
