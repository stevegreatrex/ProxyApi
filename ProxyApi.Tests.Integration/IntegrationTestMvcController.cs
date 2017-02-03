using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProxyApi.Tests.Integration
{
	public class IntegrationTestMvcController : Controller
	{
		public void GetData(string param1, int param2, bool param3)
		{}

		public void GetDataWithParametersObject(Parameters parameters)
		{}

		[HttpPost]
		public void SendData(dynamic data)
		{}

		[ProxyName("putData")]
		[HttpPut]
		public void SendDataWithPut(int id, [System.Web.Http.FromBody]dynamic data)
		{}

		public void Delete(int id)
		{}

		public void WithoutParameters()
		{}
	}
}
