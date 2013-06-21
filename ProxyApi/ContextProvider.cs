using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace ProxyApi
{
	/// <summary>
	/// An class for objects that provide the various context objects used by MVC and WebAPI
	/// </summary>
	[Export(typeof(IContextProvider))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	[ExcludeFromCodeCoverage]
	public class ContextProvider : IContextProvider
	{
		/// <summary>
		/// Gets a <see cref="HttpContextBase" /> instance representing the current HTTP context.
		/// </summary>
		/// <returns>
		/// An <see cref="HttpContextBase" /> instance.
		/// </returns>
		public HttpContextBase GetHttpContextBase()
		{
			return new HttpContextWrapper(HttpContext.Current);
		}

		/// <summary>
		/// Gets the current identity.
		/// </summary>
		/// <returns>
		/// The current authenticated identity.
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public IIdentity GetCurrentIdentity()
		{
			return HttpContext.Current.User.Identity;
		}
	}
}
