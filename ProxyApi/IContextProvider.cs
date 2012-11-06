using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ProxyApi
{
	/// <summary>
	/// An interface for objects that provide the various context objects used by MVC and WebAPI
	/// </summary>
	public interface IContextProvider
	{
		/// <summary>
		/// Gets a <see cref="HttpContextBase"/> instance representing the current HTTP context.
		/// </summary>
		/// <returns>An <see cref="HttpContextBase"/> instance.</returns>
		HttpContextBase GetHttpContextBase();
	}
}
