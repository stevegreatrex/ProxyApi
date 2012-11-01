using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProxyApi
{
	/// <summary>
	/// A wrapper class around the <see cref="VirtualPathUtility"/> class.
	/// </summary>
	[Export(typeof(IPathUtility))]
	[ExcludeFromCodeCoverage]
	public class PathUtility : IPathUtility
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
			return VirtualPathUtility.ToAbsolute(path);
		}
	}
}
