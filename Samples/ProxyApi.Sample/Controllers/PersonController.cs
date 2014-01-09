using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProxyApi.Sample.Models;

namespace ProxyApi.Sample.Controllers
{
    
	//[ValidateHttpAntiForgeryToken(ExcludeAuthenticationTypes="Basic")]
    public class PersonController : ApiController
    {


        //[HttpGet]
        //public Person GetPerson([FromUri]Person person)
        //{
        //    return person;
        //}

        [HttpGet]
        public Person GetPerson2(int id, string name)
        {
            return new Person { Id = 1, FirstName = "John", LastName = "Smith" };
        }


        public HttpResponseMessage Post([FromBody]Person person)
        {
            var p = new Person { Id = 1, FirstName = "John", LastName = "Smith" };

            var r = Request.CreateResponse<Person>(HttpStatusCode.NotModified, p);
       //     r.Content = new StringContent("SSS");

            return r;
        }


        public HttpResponseMessage Put(int id, [FromBody]Person person)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

      
        public IEnumerable<Person> Get()
        {
            var people = new List<Person>
            {
                new Person { Id = 1, FirstName = "John", LastName = "Smith" },
                new Person { Id = 2, FirstName = "James", LastName = "Bond" }
            };

            return people;
        }

        public HttpResponseMessage Get(int id)
        {
            if (id == 1)
            {
                var r = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person not found");
                r.ReasonPhrase = "Person with id " + id + " not found";
                return r;
            }
            return Request.CreateResponse<Person>(HttpStatusCode.OK,  new Person { Id = 1, FirstName = "John", LastName = "Smith" });
        }

     

        public HttpResponseMessage Delete(int id)
        {


            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
