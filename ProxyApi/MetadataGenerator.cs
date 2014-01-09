using ProxyApi.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
    //[Export(typeof(IMetadataGenerator))]
    //public class MetadataGenerator:IMetadataGenerator
    //{
    //    private IControllerDefinitionFactory _factory;
    //    private IControllerElementsProvider _typesProvider;

    //    [ImportingConstructor]
    //    public MetadataGenerator(
    //        IControllerElementsProvider typesProvider,
    //        IControllerDefinitionFactory factory)
    //    {
    //        if (typesProvider == null) throw new ArgumentNullException("typesProvider");
    //        if (factory == null) throw new ArgumentNullException("factory");

    //        _typesProvider	= typesProvider;
    //        _factory		= factory;
    //    }

    //    public IEnumerable<ContractInfo> Generate()
    //    {
    //        var controllers = _typesProvider.GetControllerTypes()
    //            .Select(_factory.Create)
    //            .ToList();
    //    }
    //}
}
