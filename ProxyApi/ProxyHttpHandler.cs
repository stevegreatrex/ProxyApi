using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;
using ProxyApi.Templates;

namespace ProxyApi
{
	[Export]
	public class ProxyHttpHandler : IHttpHandler
	{
		private static string _proxyJs;
		private static volatile object _syncRoot = new object();
		private IProxyGenerator _generator;

		[ImportingConstructor]
		public ProxyHttpHandler(IProxyGenerator generator)
		{
			_generator = generator;
		}

		private string GetProxyJs(HttpContext context)
		{
			if (_proxyJs == null)
				lock(_syncRoot)
					if(_proxyJs == null)
					{
						_proxyJs = _generator.GenerateProxyScript();
					}

			return _proxyJs;
		}

		public bool IsReusable
		{
			get { return true; }
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "application/x-javascript";
			context.Response.Write(GetProxyJs(context));
		}
	}
}
