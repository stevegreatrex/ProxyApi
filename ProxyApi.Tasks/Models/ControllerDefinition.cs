using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProxyApi.Tasks.Models
{
	/// <summary>
	/// A class representing information about a discovered controller.
	/// </summary>
	public class ControllerDefinition
	{
		

		/// <summary>
		/// Gets the name of the controller.
		/// </summary>
        [JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets the of the controller to be used in URLs
		/// </summary>
		/// <value>
		/// The URL controller name.
		/// </value>
        [JsonProperty("urlName")]
        public string UrlName { get; set; }
		
		/// <summary>
		/// Gets the action methods that appear on the controller.
		/// </summary>
		/// <value>
		/// The action methods.
		/// </value>
        [JsonProperty("actionMethods")]
        public ActionMethodDefinition[] ActionMethods { get; private set; }
		
		/// <summary>
		/// Gets the type of the controller
		/// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }


        [JsonProperty("models")]
        public ModelDefinition[] Models { get; set; }

       

        
    }
}
