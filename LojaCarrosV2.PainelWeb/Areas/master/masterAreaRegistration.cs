using System.Web.Mvc;

namespace LojaCarrosV2.PainelWeb.Areas.master
{
    public class masterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "master";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "master_default",
                "master/{controller}/{action}/{id}",
                new { controller = "Empresa", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "LojaCarrosV2.PainelWeb.Areas.master.Controllers" }

            );
        }
    }
}