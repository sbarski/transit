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
using Transit.Service.Account;
using SimpleInjector.Extensions;

namespace Transit
{
    public class InjectorConfig
    {
        public static void Register()
        {
            var container = new Container();

            RegisterRavenDB(container);

            RegisterTransitServices(container);

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

        private static void RegisterTransitServices(Container container)
        {
            var serviceAssembly = typeof(IAccountService).Assembly;

            var registrations = 
                from type in serviceAssembly.GetExportedTypes()
                where type.GetInterfaces().Length > 0
                select new
                {
                    Service = type.GetInterfaces().First(),
                    Implementation = type
                };

            foreach(var registration in registrations)
            {
                NonGenericRegistrationsExtensions.Register(container, registration.Service, registration.Implementation);
            }
        }
    }
}