using System.Web;
using System.Web.Mvc;
using Transit.Web.Attributes;

namespace Transit
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}