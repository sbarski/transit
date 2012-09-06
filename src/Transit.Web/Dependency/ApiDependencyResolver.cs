using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Transit.Web.Dependency
{
    public static partial class SimpleInjectorDependencyResolver
    {
        private sealed class SimpleInjectionHttpDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
        {
            public Container Container { get; set; }

            public System.Web.Http.Dependencies.IDependencyScope BeginScope()
            {
                return this;
            }

            public object GetService(Type serviceType)
            {
                return ((IServiceProvider)Container).GetService(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return Container.GetAllInstances(serviceType);
            }

            public void Dispose() { }
        }

        private sealed class SimpleInjectorHttpFilterAttributeFilterProvider : System.Web.Http.Filters.IFilterProvider 
        {
            private readonly Container _container;

            public SimpleInjectorHttpFilterAttributeFilterProvider(Container container)
                : base()
            {
                _container = container;
            }

            public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
            {
                if (configuration == null)
                {
                    throw new ArgumentNullException("API Filter Configuration");
                }

                if (actionDescriptor == null)
                {
                    throw new ArgumentNullException("API Filter Action Description");
                }

                foreach (var filter in actionDescriptor.GetFilters())
                {
                    _container.InjectProperties(filter);
                    yield return new FilterInfo(filter, FilterScope.Action);
                }
            }
        }
    }
}
