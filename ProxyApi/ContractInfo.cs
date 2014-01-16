using ProxyApi.ElementDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
    public class ContractInfo
    {
        public List<ContractMethodDefinition> Methods { get; set; }
        public List<ModelDefinition> Classes { get; set; }

        public ContractInfo()
        {
            Methods = new List<ContractMethodDefinition>();
            Classes = new List<ModelDefinition>();
        }

        public bool ContainsClass(string name)
        {
            if (Classes.FirstOrDefault(c => c.Name.Equals(name)) == null)
                return false;

            return true;
        }
    }
}
