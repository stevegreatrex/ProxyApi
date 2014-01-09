using ProxyApi.ElementDefinitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProxyApi
{
    [Export]
    public class MetadataMessageHandler : DelegatingHandler
    {
        private IProxyGenerator _generator;

        [ImportingConstructor]
        public MetadataMessageHandler(IProxyGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException("generator");

            _generator = generator;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {

          //  HttpContext.Current = Thread.
            return await Task.Run<HttpResponseMessage>(() =>
            {
                

                return request.CreateResponse<IEnumerable<IControllerDefinition>>(System.Net.HttpStatusCode.OK, _generator.Controllers);


            });
        }
    }
}
