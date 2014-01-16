using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi
{
    public static class StringExtensions
    {
        public static string ToCamelCasing(this string helper)
        {
            return helper.Replace(helper[0].ToString(), helper[0].ToString().ToLower());
        }
    }
}
