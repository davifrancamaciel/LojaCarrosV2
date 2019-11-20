using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LojaCarrosV2.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var formato = GlobalConfiguration.Configuration.Formatters;
            var jsonFormato = formato.JsonFormatter;
            var setings = jsonFormato.SerializerSettings;

            // preservar as refencias do objeto
            jsonFormato.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            //removendo o modeo de xml
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            setings.Formatting = Formatting.Indented;
            // traser o nome das propriedades em camel case
            setings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
