﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Transit.Web.Security
{
    public class BasicAuthMessageHandler : DelegatingHandler
    {
        private const string BasicAuthResponseHeader = "WWW-Authenticate";
        private const string BasicAuthResponseHeaderValue = "Basic";

        public AuthenticatedPrincipalProvider PrincipalProvider { get; set; }

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authValue = request.Headers.Authorization;

            if (authValue != null && !String.IsNullOrWhiteSpace(authValue.Parameter))
            {
                Credentials parsedCredentials = ParseAuthorizationHeader(authValue.Parameter);

                if (parsedCredentials != null)
                {
                    Thread.CurrentPrincipal = PrincipalProvider.CreatePrincipal(parsedCredentials.Username, parsedCredentials.Password);
                }

                return base.SendAsync(request, cancellationToken);
            }

            //return base.SendAsync(request, cancellationToken);
            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Authorization Failed")};
                return response;
            });
        }

        private Credentials ParseAuthorizationHeader(string authHeader)
        {
            string[] credentials = Encoding.ASCII.GetString(Convert
                                                            .FromBase64String(authHeader))
                                                            .Split(
                                                            new[] { ':' });

            if (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[1])) return null;

            return new Credentials()
            {
                Username = credentials[0],
                Password = credentials[1],
            };
        }
    }
}
