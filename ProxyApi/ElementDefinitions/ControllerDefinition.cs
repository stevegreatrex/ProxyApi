﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProxyApi.ElementDefinitions
{
	/// <summary>
	/// A class representing information about a discovered controller.
	/// </summary>
	public class ControllerDefinition : IControllerDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerDefinition" /> class.
		/// </summary>
		public ControllerDefinition()
		{
			this.ActionMethods = new List<IActionMethodDefinition>();
            this.Models = new List<IModelDefinition>();
		}

        public ControllerDefinition(List<IActionMethodDefinition> actionMethods, List<IModelDefinition> models)
        {
         
            this.ActionMethods = actionMethods;
            this.Models = models;
        }

		/// <summary>
		/// Gets the name of the controller.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets the of the controller to be used in URLs
		/// </summary>
		/// <value>
		/// The URL controller name.
		/// </value>
		public string UrlName { get; set; }
		
		/// <summary>
		/// Gets the action methods that appear on the controller.
		/// </summary>
		/// <value>
		/// The action methods.
		/// </value>
		public IList<IActionMethodDefinition> ActionMethods { get; private set; }
		
		/// <summary>
		/// Gets the type of the controller
		/// </summary>
		public ControllerType Type { get; set; }


        public IList<IModelDefinition> Models { get; set; }

        public bool ContainsModel(string name)
        {
            return Models.Any(c => c.Name.Equals(name));
        }

        public string Host
        {
            get
            {
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
            }
        }

        
    }
}
