using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{    
    public class ContractMethodDefinition
    {
        public string Name { get; set; }
        public Dictionary<string,string> Parameters { get; set; }
        public string RequestType { get; set; }
        public string ReturnType { get; set; }

        public ContractMethodDefinition()
        {
            Parameters = new Dictionary<string, string>();
        }
    }
}
