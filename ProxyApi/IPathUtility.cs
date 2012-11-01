using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
	/// <summary>
	/// An interface for classes that provide utility methods around virtual paths.
	/// </summary>
	public interface IPathUtility
	{
		/// <summary>
		/// Converts <paramref name="path"/> to an absolute path.
		/// </summary>
		/// <param name="path">The virtual path.</param>
		/// <returns>The absolute equivalent of the virtual path.</returns>
		string ToAbsolute(string path);
	}
}
