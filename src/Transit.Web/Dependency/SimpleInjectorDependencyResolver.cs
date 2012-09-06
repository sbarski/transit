using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Transit.Web.Dependency
{
    public static partial class SimpleInjectorDependencyResolver
    {
        public static void RegisterAsDependencyResolver(this Container container, HttpConfiguration configuration)
        {
            if (container == null)
            {
                throw new ArgumentNullException("Simple Injector Container");
            }

            var resolver = new SimpleInjectionDependencyResolver 
            {
                Container = container
            };

            var httpResolver = new SimpleInjectionHttpDependencyResolver
            {
                Container = container
            };

            DependencyResolver.SetResolver(resolver); //This is required for MVC

            configuration.DependencyResolver = httpResolver; //This is required for API
        }

        public static void RegisterAttributeFilterProvider(this Container container, HttpConfiguration configuration)
        {
            if (container == null)
            {
                throw new ArgumentNullException("Simple Injector Container");
            }

            var providers = FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().ToList();

            providers.ForEach(p => FilterProviders.Providers.Remove(p));

            FilterProviders.Providers.Add(new SimpleInjectorFilterAttributeFilterProvider(container));


            //apparently -> http://www.strathweb.com/2012/06/control-the-execution-order-of-your-filters-in-asp-net-web-api/
            configuration.Services.Add(typeof(System.Web.Http.Filters.IFilterProvider), new SimpleInjectorHttpFilterAttributeFilterProvider(container));

            var defaultProvider = configuration.Services.GetFilterProviders().First(m => m is ActionDescriptorFilterProvider);

            configuration.Services.Remove(typeof(System.Web.Http.Filters.IFilterProvider), defaultProvider);
        }
    }
}
