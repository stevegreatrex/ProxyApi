using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyApi.Tasks.Models
{
	/// <summary>
	/// A class representing the argument to a method
	/// </summary>
	public class ParameterDefinition 
    {
		/// <summary>
		/// Gets or sets the name of the argument.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

		/// <summary>
		/// Gets or sets the position at which the argument appears in the parameters list.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
	}
}
