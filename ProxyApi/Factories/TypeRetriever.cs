using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProxyApi.Factories
{
    //public class TypeRetriever:ITypeRetriever
    //{
    //    private List<string> _classesAdded;
    //    private ContractInfo _cInfo = null;

    //    public TypeRetriever()
    //    {
            
    //        _classesAdded = new List<string>();
    //    }

    //    public ContractInfo Create(Type type)
    //    {
    //        if (_cInfo == null)
    //        {
    //            Thread.BeginCriticalRegion();
    //            if (_cInfo == null)
    //            {
    //                _cInfo = new ContractInfo();
    //                _cInfo.Methods = GetContractMethodInfo(typ);
    //            }
    //            Thread.EndCriticalRegion();
    //        }

    //        return _cInfo;
    //    }

    //    private List<ContractMethodDefinition> GetContractMethodInfo(Type typ)
    //    {
    //        List<ContractMethodDefinition> res = new List<ContractMethodDefinition>();

    //        var methods = typ.GetMethods().Where(m => m.GetCustomAttribute(typeof(ExposeProxyAttribute)) != null)
    //                                     // .OrderByDescending(m=>m.GetParameters().Count())
    //                                      .ToArray();

    //        //For each method found in the controller that has a TransferContractAttribute 
    //        foreach (var method in methods)
    //        {
    //            ContractMethodDefinition tmp = new ContractMethodDefinition();

    //            tmp.Name = method.Name;

    //            ExposeProxyAttribute contractAttribute = (ExposeProxyAttribute)method.GetCustomAttribute(typeof(ExposeProxyAttribute));
    //            //Get the request type (default is "GET")
    //            tmp.RequestType = contractAttribute.RequestType.ToString();

    //            //Check is the TransferContractAttribute has a Json return type (used with MVC controllers)
    //            if(contractAttribute.JsonReturnType == null)
    //                tmp.ReturnType = AnalyseType(method.ReturnType);
    //            else
    //                tmp.ReturnType = AnalyseType(contractAttribute.JsonReturnType);

    //            ParameterInfo[] parameters = method.GetParameters();

    //            //Add parameters to the param list
    //            foreach (ParameterInfo parameter in parameters)
    //            {                    
    //                tmp.Parameters.Add(parameter.Name, AnalyseType(parameter.ParameterType));
    //            }

    //            res.Add(tmp);
    //        }

    //        return res;
    //    }

    //    private string AnalyseType(Type typ)
    //    {
    //        string res;

    //        //If the type is a generic type format to correct class name
    //        if (typ.IsGenericType)
    //        {
    //            res = typ.Name;

    //            int index = res.IndexOf('`');

    //            if (index > -1)
    //                res = res.Substring(0, index);

    //            Type[] args = typ.GetGenericArguments();

    //            res += "<";

    //            for (int i = 0; i < args.Length; i++)
    //            {
    //                if (i > 0)
    //                    res += ", ";
    //                //Recursivly find nested arguments
    //                res += AnalyseType(args[i]);
    //            }
    //            res += ">";
    //        }
    //        else
    //        {
    //            if (typ.ToString().StartsWith("System."))
    //            {
    //                if (typ.ToString().Equals("System.Void"))
    //                    res = "void";
    //                else
    //                    res = typ.Name;
    //            }
    //            else
    //            {
    //                res = typ.Name;

    //                if (!_classesAdded.Contains(typ.Name))
    //                    AddClassDefinition(typ);
    //            }
    //        }

    //        return res;
    //    }

    //    private void AddClassDefinition(Type classToDef)
    //    {
    //        //When the class is an array redefine the classToDef as the array type
    //        if (classToDef.IsArray)
    //        {
    //            classToDef = classToDef.GetElementType();
    //        }
    //        //If the class has not been mapped then map into metadata
    //        if (!_cInfo.ContainsClass(classToDef.Name))
    //        {
    //            _classesAdded.Add(classToDef.Name);

    //            ModelDefinition res = new ModelDefinition();

    //            PropertyInfo[] properties = classToDef.GetProperties();

    //            res.Name = classToDef.Name;

    //            foreach (PropertyInfo property in properties)
    //            {
    //                res.Data.Add(property.Name, AnalyseType(property.PropertyType));
    //            }

    //            _cInfo.Classes.Add(res);
    //        }
    //    }
    //}
}
