using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ProxyApi.ElementDefinitions;
using ProxyApi.Reflection;

namespace ProxyApi.Factories
{
	/// <summary>
	/// A factory class implementation used to create instances
	/// of <see cref="IActionMethodDefinition"/>.
	/// </summary>
	[Export(typeof(IActionMethodDefinitionFactory))]
	public class ActionMethodDefinitionFactory : IActionMethodDefinitionFactory
	{
		private IPathUtility _pathUtility;

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionMethodDefinitionFactory" /> class.
		/// </summary>
		/// <param name="pathUtility">The path utility.</param>
		[ImportingConstructor]
		public ActionMethodDefinitionFactory(IPathUtility pathUtility)
		{
			if (pathUtility == null) throw new ArgumentNullException("pathUtility");

			_pathUtility = pathUtility;
		}

		#endregion

		#region IActionMethodDefinitionFactory Members

		/// <summary>
		/// Creates a new <see cref="IActionMethodDefinition"/> in the context of <paramref name="controllerDefinition"/>
		/// for <paramref name="method"/>.
		/// </summary>
		/// <param name="controllerDefinition">The controller definition in which the method is found.</param>
		/// <param name="method">The method for which to create a definition.</param>
		/// <returns>A new <see cref="IActionMethodDefinition"/> instance.</returns>
		public IActionMethodDefinition Create(IControllerDefinition controllerDefinition, MethodInfo method)
		{
			if (controllerDefinition	== null) throw new ArgumentNullException("controllerDefinition");
			if (method					== null) throw new ArgumentNullException("method");

			var definition	= new ActionMethodDefinition();
			definition.Name	= method.GetProxyName();
			definition.Type = GetMethodType(method);
			definition.Url	= GetUrl(controllerDefinition, GetExplicitActionName(method) ?? method.Name.ToLower());

			var index		= 0;
			var parameters	= method.GetParameters();
			foreach (var param in parameters)
			{
				var record = new ParameterDefinition {
					Index = index,
					Name = param.Name,
					Type = param.ParameterType
				};
				if (param.HasAttribute<FromUriAttribute>())
					definition.UrlParameters.Add(record);
				else if (param.HasAttribute<FromBodyAttribute>())
					definition.BodyParameter = record;
				else if (definition.Type == HttpVerbs.Get)
					definition.UrlParameters.Add(record);
				else if (definition.Type == HttpVerbs.Post && parameters.Length == 1)
					definition.BodyParameter = record;
				else
					definition.UrlParameters.Add(record);

				index++;
			}

			return definition;
		}

		#endregion

		#region Private Members

        private string GetExplicitActionName(MethodInfo method)
        {
            var mvcActionName = method.GetCustomAttribute<System.Web.Mvc.ActionNameAttribute>(true);
            if (mvcActionName != null) return mvcActionName.Name;

            var webApiActionName = method.GetCustomAttribute<System.Web.Http.ActionNameAttribute>(true);
            if (webApiActionName != null) return webApiActionName.Name;

            return null;
        }

		private HttpVerbs GetMethodType(MethodInfo method)
		{
			if (method.HasAttribute<System.Web.Http.HttpGetAttribute>() || method.HasAttribute<System.Web.Mvc.HttpGetAttribute>()) return HttpVerbs.Get;
			if (method.HasAttribute<System.Web.Http.HttpPostAttribute>() || method.HasAttribute<System.Web.Mvc.HttpPostAttribute>()) return HttpVerbs.Post;
			if (method.HasAttribute<System.Web.Http.HttpDeleteAttribute>() || method.HasAttribute<System.Web.Mvc.HttpDeleteAttribute>()) return HttpVerbs.Delete;
			if (method.HasAttribute<System.Web.Http.HttpPutAttribute>() || method.HasAttribute<System.Web.Mvc.HttpPutAttribute>()) return HttpVerbs.Put;

			var acceptVerbs = method.GetCustomAttribute<System.Web.Http.AcceptVerbsAttribute>();
			if (acceptVerbs != null && acceptVerbs.HttpMethods.Any())
				return acceptVerbs.HttpMethods.Select(s => (HttpVerbs)Enum.Parse(typeof(HttpVerbs), s.ToString(), true)).First();

			var acceptVerbsMvc = method.GetCustomAttribute<System.Web.Mvc.AcceptVerbsAttribute>();
			if (acceptVerbsMvc != null && acceptVerbsMvc.Verbs.Any())
				return acceptVerbsMvc.Verbs.Select(s => (HttpVerbs)Enum.Parse(typeof(HttpVerbs), s.ToString(), true)).First();

			var name = method.Name.ToLower();
			if (name.StartsWith("get")) return HttpVerbs.Get;
			if (name.StartsWith("post")) return HttpVerbs.Post;
			if (name.StartsWith("delete")) return HttpVerbs.Delete;
			if (name.StartsWith("put")) return HttpVerbs.Put;

			return HttpVerbs.Get;
		}

		private string GetUrl(IControllerDefinition controller, string action)
		{
			var routeValues = new RouteValueDictionary();
			routeValues.Add("controller", controller.UrlName);
			routeValues.Add("action", action);

			if (controller.Type == ControllerType.WebApi)
				return _pathUtility.ToAbsolute(string.Format("~/api/proxy/{0}/{1}", controller.UrlName, action));
			else
				return _pathUtility.ToAbsolute(_pathUtility.GetVirtualPath(routeValues));
		}

		#endregion
	}
}
