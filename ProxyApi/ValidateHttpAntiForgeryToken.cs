using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace ProxyApi
{
	 /// <summary>
    /// Validates Anti-Forgery CSRF tokens for Web API based on a cookie and a header value.
    /// </summary>
    /// <remarks>
    /// Adapted from the MVC 4 SPA template
    /// </remarks>
    [ExcludeFromCodeCoverage] //adapted from SPA template
	public class ValidateHttpAntiForgeryTokenAttribute : AuthorizationFilterAttribute
	{
		public const string RequestVerificationTokenHeader = "X-RequestVerificationToken";

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			var request = actionContext.ControllerContext.Request;

			try
			{
				if (ShouldValidateRequest(actionContext))
					ValidateRequestHeader(request);
			}
			catch (HttpAntiForgeryException e)
			{
				actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, e);
			}
		}

		/// <summary>
		/// A list of authentication types for which the token will
		/// not be checked.
		/// </summary>
		public string[] ExcludeAuthenticationType { get; set; }

		/// <summary>
		/// Determines whether or not a request should be validated
		/// </summary>
		/// <param name="actionContext">The action context.</param>
		/// <returns></returns>
		protected virtual bool ShouldValidateRequest(HttpActionContext actionContext)
		{
			var identity = HttpContext.Current.User.Identity;
			if (this.ExcludeAuthenticationType != null)
			{
				if (identity.IsAuthenticated && this.ExcludeAuthenticationType.Contains(identity.AuthenticationType))
					return false;
			}

			return true;
		}

		private void ValidateRequestHeader(HttpRequestMessage request)
		{
			var formToken   = string.Empty;
			var cookieToken = request.Headers
                .GetCookies()
                .Select(c => c[AntiForgeryConfig.CookieName])
				.Select(c => c.Value)
                .FirstOrDefault();

			IEnumerable<string> tokenHeaders;
			if (request.Headers.TryGetValues(RequestVerificationTokenHeader, out tokenHeaders))
				formToken = tokenHeaders.FirstOrDefault();

			AntiForgery.Validate(cookieToken, formToken);
		}
	}
}
