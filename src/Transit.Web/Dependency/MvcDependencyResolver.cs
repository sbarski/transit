using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Http;

namespace Transit.Web.Dependency
{
    public static partial class SimpleInjectorDependencyResolver
    {
        private sealed class SimpleInjectionDependencyResolver : System.Web.Mvc.IDependencyResolver
        {
            public Container Container { get; set; }

            public object GetService(Type serviceType)
            {
                return ((IServiceProvider)Container).GetService(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return Container.GetAllInstances(serviceType);
            }
        }

        private sealed class SimpleInjectorFilterAttributeFilterProvider : FilterAttributeFilterProvider
        {
            private readonly Container _container;

            public SimpleInjectorFilterAttributeFilterProvider(Container container)
                : base(false)
            {
                _container = container;
            }

            public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
            {
                if (controllerContext == null)
                {
                    throw new ArgumentNullException("MVC Filter Controller");
                }

                if (actionDescriptor == null)
                {
                    throw new ArgumentNullException("MVC Filter Action Description");
                }

                var filters = base.GetFilters(controllerContext, actionDescriptor).ToArray();

                for (int i = 0; i < filters.Length; i++)
                {
                    _container.InjectProperties(filters[i].Instance);
                    yield return filters[i];
                }
            }
        }
    }
}
