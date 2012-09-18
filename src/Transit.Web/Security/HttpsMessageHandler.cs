using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transit.Web.Security
{
    public class HttpsMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!String.Equals(request.RequestUri.Scheme, "https", StringComparison.OrdinalIgnoreCase))
            {
                return Task.Factory.StartNew(() =>
                    {
                        return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Transit Requires HTTPS") };
                    });
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
