using System.Web;
using System.Web.Mvc;

namespace CoreTechs_CodeTest_10_24_2016
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
