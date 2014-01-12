using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Tasks
{
public class ProxyGenerationTask:ToolTask
    {
    
        
        [Output]
        public string Filename { get; set; }
        public string Path { get; set; }
        protected override string GenerateFullPathToTool()
        {
            return Path + "\\ProxyApi.MetadataGenerator.exe";
        }

        
        public ITaskItem[] ConfigurationFiles { get; set; }

        protected override string ToolName
        {
            get { return "ProxyGenerationTask"; }
        }

        protected override string GenerateCommandLineCommands()
        {
            CommandLineBuilder builder = new CommandLineBuilder();

          //  var configs = String.Join<ITaskItem>("\",\"", ConfigurationFiles);

            

            

            if (ConfigurationFiles != null)
            {
               // builder.AppendSwitch("/config:");
                
                builder.AppendFileNamesIfNotNull(ConfigurationFiles, ",");
            }

            if (this.Filename != null)
            {
                builder.AppendSwitch("/output:\"" + this.Filename + "\"");
            }

            Log.LogMessage(builder.ToString());

            // We have all of our switches added, return the commandline as a string
            return builder.ToString();
        }
    }
}

