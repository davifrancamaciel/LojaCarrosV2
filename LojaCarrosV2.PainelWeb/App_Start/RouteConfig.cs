using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LojaCarrosV2.PainelWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Excluir Arquivo Galeria",
                "excluir/{id}/{nome}/{idVeiculo}",
                new { controller = "Veiculos", action = "ExcluirArquivo" },
               namespaces: new[] { "LojaCarrosV2.PainelWeb.Controllers" }
            );
            routes.MapRoute(
                name: "olx",
                url: "code",
                defaults: new { controller = "Olx", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "LojaCarrosV2.PainelWeb.Controllers" }
            );

            routes.MapRoute(
                name: "Veiculos",
                url: "veiculos",
                defaults: new { controller = "Veiculos", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "LojaCarrosV2.PainelWeb.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "usuario", action = "Index", id = UrlParameter.Optional },
                defaults: new { controller = "Veiculos", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "LojaCarrosV2.PainelWeb.Controllers" }
            );
        }
    }
}
