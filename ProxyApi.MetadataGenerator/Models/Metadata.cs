using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.MetadataGenerator.Models
{
    public class Metadata
    {
        public string Host { get; set; }

        [JsonProperty("controllers")]
        public IEnumerable<ControllerDefinition> Definitions { get; set; }
    }
}
