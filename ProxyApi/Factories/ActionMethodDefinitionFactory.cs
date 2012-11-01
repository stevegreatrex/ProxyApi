using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ProxyApi.ElementDefinitions;
using ProxyApi.Reflection;

namespace ProxyApi
{
	[Export(typeof(IActionMethodDefinitionFactory))]
	public class ActionMethodDefinitionFactory : IActionMethodDefinitionFactory
	{
		private IPathUtility _pathUtility;

		[ImportingConstructor]
		public ActionMethodDefinitionFactory(IPathUtility pathUtility)
		{
			_pathUtility = pathUtility;
		}

		public ActionMethodDefinition Create(ControllerDefinition controllerDefinition, MethodInfo method)
		{
			var definition = new ActionMethodDefinition();
			definition.Name	= method.Name.ToLower();
			definition.Type = GetMethodType(method);
			definition.Url	= GetUrl(controllerDefinition, definition.Name);

			var index = 0;
			var parameters = method.GetParameters();
			foreach (var param in parameters)
			{
				var record = new ParameterDefinition { Index = index, Name = param.Name };
				if (param.HasAttribute<FromUriAttribute>())
					definition.UrlParameters.Add(record);
				else if (param.HasAttribute<FromBodyAttribute>())
					definition.BodyParameter = record;
				else if (definition.Type == HttpMethod.Get)
					definition.UrlParameters.Add(record);
				else if (definition.Type == HttpMethod.Post && parameters.Length == 1)
					definition.BodyParameter = record;
				else
					definition.UrlParameters.Add(record);

				index++;
			}

			return definition;
		}

		private HttpMethod GetMethodType(MethodInfo method)
		{
			if (method.HasAttribute<HttpGetAttribute>() || method.HasAttribute<System.Web.Mvc.HttpGetAttribute>()) return HttpMethod.Get;
			if (method.HasAttribute<HttpPostAttribute>() || method.HasAttribute<System.Web.Mvc.HttpPostAttribute>()) return HttpMethod.Post;
			if (method.HasAttribute<HttpDeleteAttribute>() || method.HasAttribute<System.Web.Mvc.HttpDeleteAttribute>()) return HttpMethod.Delete;
			if (method.HasAttribute<HttpPutAttribute>() || method.HasAttribute<System.Web.Mvc.HttpPutAttribute>()) return HttpMethod.Put;

			var acceptVerbs = method.GetCustomAttribute<AcceptVerbsAttribute>();
			if (acceptVerbs != null && acceptVerbs.HttpMethods.Any())
				return acceptVerbs.HttpMethods.First();

			var name = method.Name.ToLower();
			if (name.StartsWith("get")) return HttpMethod.Get;
			if (name.StartsWith("post")) return HttpMethod.Post;
			if (name.StartsWith("delete")) return HttpMethod.Delete;
			if (name.StartsWith("put")) return HttpMethod.Put;

			return HttpMethod.Get;
		}

		private string GetUrl(ControllerDefinition controller, string action)
		{	
			if (controller.Type == ControllerType.WebApi)
				return _pathUtility.ToAbsolute(string.Format("~/api/proxy/{0}/{1}", controller.Name, action));
			else
				return _pathUtility.ToAbsolute(string.Format("~/proxy/{0}/{1}", controller.Name, action));
		}
	}
}
