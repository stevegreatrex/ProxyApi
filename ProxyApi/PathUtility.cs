using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace ProxyApi
{
	/// <summary>
	/// A wrapper class around the <see cref="VirtualPathUtility"/> class.
	/// </summary>
	[Export(typeof(IPathUtility))]
	[ExcludeFromCodeCoverage]
	public class PathUtility : IPathUtility
	{
		private IContextProvider _contextProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="PathUtility" /> class.
		/// </summary>
		/// <param name="contextProvider">The context provider.</param>
		[ImportingConstructor]
		public PathUtility(IContextProvider contextProvider)
		{
			_contextProvider = contextProvider;
		}

		/// <summary>
		/// Converts <paramref name="path" /> to an absolute path.
		/// </summary>
		/// <param name="path">The virtual path.</param>
		/// <returns>
		/// The absolute equivalent of the virtual path.
		/// </returns>
		public string ToAbsolute(string path)
		{
			return VirtualPathUtility.ToAbsolute(path);
		}


		/// <summary>
		/// Gets the virtual path for the route specified by the values in <paramref name="routeValues" />.
		/// </summary>
		/// <param name="routeValues">The route values dictionary.</param>
		/// <returns>
		/// The virtual path for the route.
		/// </returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public string GetVirtualPath(RouteValueDictionary routeValues)
		{
			if (routeValues == null) throw new ArgumentNullException("routeValues");

			return RouteTable.Routes.GetVirtualPath(new RequestContext(_contextProvider.GetHttpContextBase(), new RouteData()), routeValues).VirtualPath;
		}
	}
}
