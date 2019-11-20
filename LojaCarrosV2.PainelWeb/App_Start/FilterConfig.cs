using System.Web;
using System.Web.Mvc;

namespace LojaCarrosV2.PainelWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
