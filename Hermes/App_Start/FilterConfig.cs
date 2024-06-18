using System.Web;
using System.Web.Mvc;
using Hermes.Filters;

namespace Hermes
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // custom exception handler
            filters.Add(new ErrorHandlerFilter());
        }
    }
}
