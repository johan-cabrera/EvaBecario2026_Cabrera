using System.Web;
using System.Web.Mvc;

namespace EvaBecario2026_Cabrera
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
