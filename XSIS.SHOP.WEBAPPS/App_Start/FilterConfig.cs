using System.Web;
using System.Web.Mvc;

namespace XSIS.SHOP.WEBAPPS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
