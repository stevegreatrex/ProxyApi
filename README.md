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
