using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Tasks.Models
{
    public class Configuration
    {
        public const string FileName = "ProxyApiConfig.json";
        public const string CacheFile = "ProxyApiSource.cache";

        private string _clientSuffix;

        [JsonProperty("clientSuffix")]
        public string ClientSuffix
        {
            get
            {
                return _clientSuffix.DefaultIfEmpty("Client");
            }
            set
            {
                _clientSuffix = value;
            }
        }

        private string _namespace;

        [JsonProperty("namespace")]
        public string Namespace
        {
            get
            {
                return _namespace.DefaultIfEmpty("ProxyApi.Proxies");
            }
            set
            {
                _namespace = value;
            }
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proxyEndpoint")]
        public string Endpoint { get; set; }

        [JsonIgnore]
        public Metadata Metadata { get; set; }

    

        public static Configuration Load()
        {
            if (!File.Exists(Configuration.FileName))
                throw new ConfigFileNotFoundException();  

            var json = File.ReadAllText(Configuration.FileName);

            return JsonConvert.DeserializeObject<Configuration>(json);

            
        }

    }
}
