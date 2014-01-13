using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Tasks.Models
{
    public class Metadata
    {
        public string Host { get; set; }

        [JsonProperty("controllers")]
        public IEnumerable<ControllerDefinition> Definitions { get; set; }

        public static Metadata LoadFromCache()
        {
            if (!File.Exists(Configuration.CacheFile))
            {
                return null;
            }
            var json = File.ReadAllText(Configuration.CacheFile);
            return JsonConvert.DeserializeObject<Metadata>(json);
        }
    }
}
