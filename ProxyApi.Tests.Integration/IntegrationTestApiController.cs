using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProxyApi.Tests.Integration
{
	[ProxyName("apiIntegrationTest")]
	public class IntegrationTestApiController : ApiController
	{
		public string GetData(string param1, int param2, bool param3)
		{
			return "test";
		}
	}
}
