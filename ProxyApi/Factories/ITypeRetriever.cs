using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Factories
{
    public interface ITypeRetriever
    {
        ContractInfo Create(Type type);
    }
}
