using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProxyApi.Sample.Models;

namespace ProxyApi.Sample.Controllers
{
	//[ValidateHttpAntiForgeryToken]
    public class PersonController : ApiController
    {
		[HttpGet]
		public IEnumerable<Person> GetAllPeople()
		{
			var people = new List<Person>
			{
				new Person { Id = 1, FirstName = "John", LastName = "Smith" },
				new Person { Id = 2, FirstName = "James", LastName = "Bond" }
			};

			return people;
		}
    }
}
