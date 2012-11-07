using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyApi
{
	/// <summary>
	/// A null-pattern implementation of <see cref="IPathUtility"/>.
	/// </summary>
	public class NullPathUtility : IPathUtility
	{
		/// <summary>
		/// Converts <paramref name="path" /> to an absolute path.
		/// </summary>
		/// <param name="path">The virtual path.</param>
		/// <returns>
		/// The absolute equivalent of the virtual path.
		/// </returns>
		public string ToAbsolute(string path)
		{
			return string.Empty;
		}

		/// <summary>
		/// Gets the virtual path for the route specified by the values in <paramref name="routeValues" />.
		/// </summary>
		/// <param name="routeValues">The route values dictionary.</param>
		/// <returns>
		/// The virtual path for the route.
		/// </returns>
		public string GetVirtualPath(System.Web.Routing.RouteValueDictionary routeValues)
		{
			return string.Empty;
		}
	}
}
