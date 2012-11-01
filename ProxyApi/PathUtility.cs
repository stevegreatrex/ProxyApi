using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProxyApi
{
	[Export(typeof(IPathUtility))]
	public class PathUtility : IPathUtility
	{
		public string ToAbsolute(string path)
		{
			return VirtualPathUtility.ToAbsolute(path);
		}
	}
}
