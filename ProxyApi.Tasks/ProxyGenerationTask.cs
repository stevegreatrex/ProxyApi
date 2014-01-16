﻿using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json;
using ProxyApi.Tasks.Models;
using ProxyApi.Tasks.Templates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Tasks
{
public class ProxyGenerationTask:ITask
    {
        private Configuration config;
        
        [Output]
        public string Filename { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            

            try
            {

                config = Configuration.Load();

                config.Metadata = GetProxy();

                var template = new CSharpProxyTemplate(config);
                //template.renderNamespaces = namespaces;

                var source = template.TransformText();

                File.WriteAllText(Filename, source);
                File.WriteAllText(Configuration.CacheFile, source);
            }
            catch (ConnectionException)
            {
                tryReadFromCache();
               // throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

            return true;
        }

      

        private Metadata GetProxy()
        {
            var url = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Add("X-Proxy-ResponseType", "metadata");
                    
                    

                    var response = client.GetAsync(config.Endpoint).Result;

                    response.EnsureSuccessStatusCode();

                    var metadata = response.Content.ReadAsAsync<Metadata>().Result;



                    return metadata;
                }
            }
            catch (Exception ex)
            {

                throw new ConnectionException(config.Endpoint);
            }
                
          
            
        }

        private void tryReadFromCache()
        {

                if (!File.Exists(Configuration.CacheFile))
                {
                    throw new ConnectionException(config.Endpoint);
                }
                var source = File.ReadAllText(Configuration.CacheFile);
                File.WriteAllText(Filename, source);

            
        }

      
    }
}

