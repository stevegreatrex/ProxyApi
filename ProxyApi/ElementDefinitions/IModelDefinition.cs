using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.ElementDefinitions
{
    public interface IModelDefinition
    {
        string Name { get; set; }
        Dictionary<string, string> Data { get; set; }
    }
}
