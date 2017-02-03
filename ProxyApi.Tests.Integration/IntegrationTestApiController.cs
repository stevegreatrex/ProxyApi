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
		public void GetData(string param1, int param2, bool param3)
		{}

		public void GetDataWithParametersObject([FromUri]Parameters parameters)
		{}

		[HttpPost]
		public void SendData(dynamic data)
		{}

		[ProxyName("putData")]
		[HttpPut]
		public void SendDataWithPut(int id, [FromBody]dynamic data)
		{}

		public void Delete(int id)
		{}

		public void WithoutParameters()
		{}
	}
}
