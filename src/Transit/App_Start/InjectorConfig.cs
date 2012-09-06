using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Raven.Client.Document;
using Transit.Data;
using SimpleInjector;
using Raven.Client;
using System.Web.Mvc;
using System.Web.Http;
using Transit.Web.Attributes;
using Transit.Web.Dependency;

namespace Transit
{
    public class InjectorConfig
    {
        public static void Register()
        {
            var container = new Container();

            RegisterRavenDB(container);

            container.Verify();

            container.RegisterAsDependencyResolver(GlobalConfiguration.Configuration);

            container.RegisterAttributeFilterProvider(GlobalConfiguration.Configuration);
        }

        private static void RegisterRavenDB(Container container)
        {
            var store = new DocumentStore { ConnectionStringName = "Server" };

            store.Initialize(); 

            container.RegisterSingle<IDocumentStore>(store);
            
            container.RegisterPerWebRequest<IAsyncDocumentSession>(() => store.OpenAsyncSession());

            container.RegisterPerWebRequest<IDocumentSession>(() => store.OpenSession());
        }
    }
}