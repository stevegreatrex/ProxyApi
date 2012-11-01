using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace ProxyApi
{
	[Export]
	public class RouteHandler : IRouteHandler
	{
		private IProxyGenerator _proxyGenerator;
		[ImportingConstructor]
		public RouteHandler(IProxyGenerator proxyGenerator)
		{
			_proxyGenerator = proxyGenerator;
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new ProxyHttpHandler(_proxyGenerator);
		}
	}
}
