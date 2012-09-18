using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Transit.Web.Security;

namespace Transit
{
    public class SecurityConfig
    {
        public static void RegisterHandlers(HttpConfiguration configuration)
        {
            configuration.MessageHandlers
              .Add(new HttpsMessageHandler());
        }
    }
}