using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.MetadataGenerator.Models
{
    public static class StringExtensions
    {
        public static string DefaultIfEmpty(this string helper, string value)
        {
            if (String.IsNullOrEmpty(helper))
                return value;

            return helper;
        }
    }
}
