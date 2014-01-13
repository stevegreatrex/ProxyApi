using Newtonsoft.Json;
using ProxyApi.MetadataGenerator.Models;
using ProxyApi.MetadataGenerator.Templates;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.MetadataGenerator
{
    class Program
    {
        private static Assembly ThisAssembly;


        static Dictionary<string,string> parseArgs(string[] args)
        {
            var typedArgs = new Dictionary<string, string>();

            if (args.Length == 0)
            {
                PrintHelp();
                return null;
            }

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                string value = string.Empty;
                bool argument = false;

                if (arg.StartsWith("/") || arg.StartsWith("-"))
                {
                    argument = true;
                    int colonPos = arg.IndexOf(":");
                    if (colonPos != -1)
                    {
                        value = arg.Substring(colonPos + 1);
                        arg = arg.Substring(1, colonPos-1);

                        typedArgs.Add(arg, value);
                    }
                }

                //if (!argument)
                //{
                //    typedArgs.Add("config", arg);
                //}

            }

            //if (!typedArgs.ContainsKey("config"))
            //    typedArgs.Add("config", "ProxyApiConfig.json");

            return typedArgs;
        }


        static int Main(string[] args)
        {
            ThisAssembly = Assembly.GetExecutingAssembly();

            var arguments = parseArgs(args);

            string csFileName = arguments["output"];
            //var configFileNames = arguments["config"].Split(',');
            
            try
            {
                StringBuilder sb = new StringBuilder();
                var namespaces = true;

                //foreach (var c in configFileNames)
                //{
                    var config = Configuration.Load();

                    if (config.Endpoint == string.Empty)
                    {
                        var frm = new frmApiProxyConfig(config);
                        frm.ShowDialog();
                    }


                    sb.AppendLine(WriteSource(config, csFileName,namespaces));
                    namespaces = false;
                //}

                File.WriteAllText(csFileName, sb.ToString());
                
            }
            catch (Exception e)
            {
                PrintErrorMessage(e.ToString());
                return 1;
            }
          //  Console.ReadKey();
            return 0;
        }

        static string WriteSource(Configuration config, string fileName, bool namespaces)
        {
           

                config.Metadata = GetProxy(config).Result;

                var template = new CSharpProxyTemplate(config);
                template.renderNamespaces = namespaces;

                return template.TransformText();

                
     
            
        }

        static async Task<Metadata> GetProxy(Configuration config)
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("X-Proxy-ResponseType", "metadata");

                var response = await client.GetAsync(config.Endpoint);

                return await response.Content.ReadAsAsync<Metadata>();                
            }
        }

        private static void PrintMessage(string csFileName)
        {
            PrintHeader();
            Console.WriteLine("Generated " + csFileName + "...");
        }

        private static void PrintErrorMessage(String e)
        {
            Console.Error.WriteLine("ProxyAPI: error TX0001: {0}", e);
        }

        private static void PrintHeader()
        {
            Console.WriteLine(String.Format(CultureInfo.CurrentCulture, "[ProxyAPI, Version {0}]", ThisAssembly.ImageRuntimeVersion));
        }
        private static void PrintHelp()
        {
            PrintHeader();
            string name = ThisAssembly.GetName().Name;
            Console.WriteLine();
            Console.WriteLine(name + " - " + "Utility to generate proxies for Web APIs");
        //    Console.WriteLine("Usage: " + name + " <configFiles> [one or more config files] [/output:<csFileName>.cs] [/lib:<assemblyName>] [/config:<configFileName>.xml] [/enableServiceReference] [/nameMangler2]");
        }

       

    }
}
