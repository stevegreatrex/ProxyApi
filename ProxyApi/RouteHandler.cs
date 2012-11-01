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
	/// <summary>
	/// A <see cref="IRouteHandler"/> instance that returns the generated proxy script.
	/// </summary>
	[Export]
	public class RouteHandler : IRouteHandler
	{
		private IProxyGenerator _proxyGenerator;

		/// <summary>
		/// Initializes a new instance of the <see cref="RouteHandler" /> class.
		/// </summary>
		/// <param name="proxyGenerator">The proxy generator.</param>
		[ImportingConstructor]
		public RouteHandler(IProxyGenerator proxyGenerator)
		{
			if (proxyGenerator == null) throw new ArgumentNullException("proxyGenerator");

			_proxyGenerator = proxyGenerator;
		}

		/// <summary>
		/// Provides the object that processes the request.
		/// </summary>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		/// <returns>
		/// An object that processes the request.
		/// </returns>
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			if (requestContext == null) throw new ArgumentNullException("requestcontext");

			return new ProxyHttpHandler(_proxyGenerator);
		}
	}
}
