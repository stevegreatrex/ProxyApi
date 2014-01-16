using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ProxyApi.Tasks.Models
{
	public class ActionMethodDefinition
	{
        [JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		/// Gets the name of the method.
		/// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

		/// <summary>
		/// Gets the MVC/WebApi URL for this definition.
		/// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

		/// <summary>
		/// Gets a list containing the URL parameters.  Parameter values are in the format [index, name]
		/// </summary>
		/// <value>
		/// The URL parameters.
		/// </value>
        [JsonProperty("urlParameters")]
        public ParameterDefinition[] UrlParameters { get; private set; }

		/// <summary>
		/// Gets a list containing the body parameters.  Parameter value is in the format [index, name]
		/// </summary>
		/// <value>
		/// The body parameters.
		/// </value>
        [JsonProperty("bodyParameter")]
        public ParameterDefinition BodyParameter { get; set; }

        [JsonProperty("returnType")]
        public string ReturnType { get; set; }


	}
}
