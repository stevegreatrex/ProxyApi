using ProxyApi.ElementDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Templates
{
    public  interface IProxyTemplate
    {
        string TransformText();
        IEnumerable<IControllerDefinition> Definitions { get; set; }
    }
}
