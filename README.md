ProxyApi is a library that automatically creates JavaScript proxy objects for your ASP.NET MVC and WebApi Controllers.

```csharp
public class DemoController : ApiController
{
    public void Post(int id, Person person) {
	}

	public Person Get(int id) {
	}
}
```
...becomes...
```javascript
$.proxies.demo.post({ Name: 'Bob Smith' });

$.proxies.demo.get(5)
  .done(function(person) {
    //use retrieved person
  });
```

All in 2 easy steps:
 1. Download the [NuGet package](https://nuget.org/packages/ProxyApi)
 2. Add a script tag for `~/api/proxies`

Intellisense Support
--------------------

If you want intellisense support for the generated proxies, you can download the additional [Intellisense NuGet package](https://nuget.org/packages/ProxyApi.Intellisense)

See the [introductory blog post](http://blog.greatrexpectations.com/2012/11/06/proxyapi-automatic-javascript-proxies-for-webapi-and-mvc/) for more details, and check out [greatrexpectations.com](http://greatrexpectations.com) for updates.