using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Tasks.Models
{
    public class ModelDefinition
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("properties")]
        public ModelProperty[] Data { get; set; }

       
    }

    public class ModelProperty
    {
        [JsonProperty("name")]
        public string Key { get; set; }
        [JsonProperty("type")]
        public string Value { get; set; }
    }
}
